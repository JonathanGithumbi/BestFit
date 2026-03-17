# Testing and Quality Onboarding Guide

## Purpose

This document explains the automated testing work added around the cart, checkout, and order-placement flow, plus the application changes that were introduced to make that test suite maintainable.

It is written for a new developer joining the team. After reading this guide, a developer should be able to:

- understand why the current test architecture looks the way it does
- run the full test suite locally
- regenerate coverage reports
- extend the existing tests without breaking the harness
- maintain the test-specific application hooks safely
- identify the major uncovered areas that still need work

Read this guide together with [Guest Cart and Checkout Implementation Guide](guest-cart-checkout.md).

## Scope of This Work

This testing work focused on the user journey that matters most in the current storefront:

- cart creation and cart mutation
- checkout state stored in session
- delivery and payment progression
- order submission to the API
- receipt display and cart clearing

The work delivered:

- one unit-test project
- one integration-test project
- one acceptance-test project
- Cobertura coverage output using `coverlet.collector`
- test-only application hooks that are disabled by default
- a few production bug fixes exposed while building the test suite

## Current Snapshot

As of 2026-03-12, the suite contains:

- 50 unit tests
- 8 integration tests
- 4 acceptance tests
- 62 passing tests total

Coverage snapshot from the latest successful run:

- Unit suite: 26.12% line coverage, 15.85% branch coverage
- Integration suite: 1.48% line coverage, 8.98% branch coverage
- Acceptance suite: 1.87% line coverage
- Combined unique line coverage across application code: 28.85% when EF migrations and designer files are excluded
- Combined unique line coverage across all instrumented files: 2.82%

The two combined numbers are both useful:

- the lower number reflects everything instrumented, including generated migration code
- the higher number better reflects the maintainable application code the team actually edits

## Big Picture Strategy

The test architecture follows a practical test pyramid:

### Unit tests

These are fast, in-process tests that isolate one service, controller, or view-model behavior at a time.

They are used for:

- session-backed services
- application services with repository doubles
- MVC controllers that can be exercised without spinning up the whole web app
- lightweight model logic

### Integration tests

These are composition tests, not full database tests.

They intentionally exercise multiple real classes together, such as:

- API controller + application service + mapper + fake repositories
- MVC controller + session services + checkout order service

They are used to verify class collaboration and request-shape assumptions without paying the cost of a full external dependency stack.

### Acceptance tests

These are end-to-end user-flow tests.

They start a real `BestFit.Web` process, talk to it over HTTP, submit forms with anti-forgery tokens, and drive the checkout experience like a browser client would.

They use a stub API server so the web app can place orders without depending on the full production API during the test run.

## Why the Integration Layer Looks Like This

The original plan included repository-level integration tests backed by SQL Server LocalDB. That approach did not hold up in the current environment because LocalDB initialization was unreliable and prevented the suite from being portable.

The current integration project therefore verifies real composition boundaries without a live database:

- API controllers call the real application service
- MVC controllers call the real web services
- AutoMapper profiles are real
- repositories and unit of work are fake

This is an important tradeoff to understand:

- the current integration suite is stable and fast
- the current integration suite does not validate EF Core mappings, SQL behavior, or repository implementations

That gap is real. It is called out again in the backlog section below.

## Directory Map

### Test projects

- `tests/BestFit.UnitTests`
- `tests/BestFit.IntegrationTests`
- `tests/BestFit.AcceptanceTests`

### Shared support inside each test project

Unit test support:

- `tests/BestFit.UnitTests/Support/Assert.cs`
- `tests/BestFit.UnitTests/Support/TestDoubles.cs`

Integration test support:

- `tests/BestFit.IntegrationTests/Support/Assert.cs`
- `tests/BestFit.IntegrationTests/Support/TestDoubles.cs`
- `tests/BestFit.IntegrationTests/Support/TestInfrastructure.cs`

Acceptance test support:

- `tests/BestFit.AcceptanceTests/Support/Assert.cs`
- `tests/BestFit.AcceptanceTests/Support/HtmlFormHelpers.cs`
- `tests/BestFit.AcceptanceTests/Support/PortAllocator.cs`
- `tests/BestFit.AcceptanceTests/Support/StubApiServer.cs`
- `tests/BestFit.AcceptanceTests/Support/WebAppHost.cs`

### Primary application files changed for testability

- `src/BestFit.Web/Program.cs`
- `src/BestFit.Web/Data/ApplicationDbContext.cs`
- `src/BestFit.Web/Services/SessionCartService.cs`
- `src/BestFit.Web/Services/CheckoutOrderService.cs`
- `src/BestFit.Web/Services/TestHeaderAuthenticationHandler.cs`
- `src/BestFit.Web/Models/Checkout/CheckoutPaymentViewModel.cs`
- `src/BestFit.Application/Services/ProductService.cs`
- `src/BestFit.Application/Services/FeaturedContentService.cs`

### Environment bootstrap files added during this work

- `global.json`
- `Directory.Build.props`

## Test Project Breakdown

## Unit Test Project

Project:

- `tests/BestFit.UnitTests`

Primary areas covered:

- cart and receipt view models
- `SessionCartService`
- `SessionCheckoutService`
- `CheckoutOrderService`
- `OrderProductService`
- `HomeService`
- `CategoryService`
- `ProductService`
- `FeaturedContentService`
- `HomeController`
- `ShopController`
- `CartController`

Primary support objects:

- `TestSession`
- `StubHttpMessageHandler`
- `TestHttpClientFactory`
- `TestWebHostEnvironment`
- `TestHttpContextAccessorFactory`
- in-memory repository implementations
- `FakeUnitOfWork`
- `StubTempDataProvider`

Why this project matters:

- it is the fastest feedback loop in the repo
- it contains most of the business-rule and controller-behavior coverage
- it is the first place to add tests when you fix a bug in a service or controller

## Integration Test Project

Project:

- `tests/BestFit.IntegrationTests`

Primary areas covered:

- `OrderProductController` + `OrderProductService` + AutoMapper
- `CheckoutController` + `SessionCartService` + `SessionCheckoutService` + `CheckoutOrderService`

Primary support objects:

- a second local `Assert` helper
- fake repositories and fake unit of work
- `TestHttpContextFactory`
- `StubTempDataProvider`
- `TestInfrastructure.CreateMapper()`

Why this project matters:

- it proves the object graph and request/response shapes work when real classes are assembled together
- it catches mismatches between controllers, DTOs, and service expectations
- it is the right place to verify cross-class behavior that would be too coupled for a unit test but too small for an acceptance test

## Acceptance Test Project

Project:

- `tests/BestFit.AcceptanceTests`

Primary areas covered:

- empty-cart checkout page
- login gate for guest checkout
- authenticated full checkout flow
- payment-step redirect when delivery is missing

How the acceptance harness works:

- `WebAppHost` starts the real `BestFit.Web` app with `dotnet run`
- `StubApiServer` starts a lightweight in-process ASP.NET Core host that mimics the API endpoints used by checkout
- `HtmlFormHelpers` extracts the anti-forgery token from rendered HTML before form submission
- test clients use cookies so session state behaves like a real browser session
- authenticated flows send test headers consumed by `TestHeaderAuthenticationHandler`

Why this project matters:

- it is the closest thing to real user behavior in the repo today
- it validates routing, middleware, anti-forgery, session state, controller actions, views, redirects, and end-to-end order placement wiring

## Test-Specific Application Hooks

These hooks were added so tests can drive the real application without affecting production behavior.

All of them are opt-in and off by default.

### `ApiUrl`

Used by:

- `SessionCartService`
- `CheckoutOrderService`

Why it exists:

- production code previously hardcoded the API base URL
- tests need to point the web layer at a local stub server

Default behavior:

- falls back to `https://localhost:7198` when the setting is missing

### `TestAuthentication:Enabled`

Used by:

- `src/BestFit.Web/Program.cs`
- `src/BestFit.Web/Services/TestHeaderAuthenticationHandler.cs`

Why it exists:

- acceptance tests need a safe way to simulate an authenticated user without going through the full login UI and identity store

Behavior when enabled:

- the default authentication scheme becomes `TestHeader`
- the handler reads:
  - `X-Test-UserId`
  - `X-Test-Email`
  - `X-Test-Name`

Behavior when disabled:

- the app uses its normal cookie-based authentication setup

### `TestHost:Enabled`

Used by:

- `src/BestFit.Web/Program.cs`

Why it exists:

- the acceptance host runs in a constrained local environment
- Windows event log and machine-level data protection settings caused startup and session failures

Behavior when enabled:

- logging is simplified
- data-protection keys are stored in a test-owned folder instead of relying on machine state
- the app can run repeatably under the acceptance harness

### `TestHost:DisableHttpsRedirection`

Used by:

- `src/BestFit.Web/Program.cs`

Why it exists:

- acceptance tests use plain `http://127.0.0.1:<port>/`
- automatic HTTPS redirection would break those flows

### `TestHost:DataProtectionDirectory`

Used by:

- `src/BestFit.Web/Program.cs`
- `tests/BestFit.AcceptanceTests/Support/WebAppHost.cs`

Why it exists:

- acceptance tests need a writable, isolated location for ASP.NET Core data-protection keys

### `ConnectionStrings:DefaultConnection`

Used by:

- `src/BestFit.Web/Program.cs`

Why it matters in tests:

- the web app still expects a connection string shape even if the acceptance path never uses the database directly
- `WebAppHost` injects a test connection string so startup remains predictable

## Production Bugs Found and Fixed While Building Tests

The test work exposed several real issues that were fixed in production code.

### `ProductService.CreateProduct` was adding a product twice

File:

- `src/BestFit.Application/Services/ProductService.cs`

Fix:

- removed the unconditional second `Add(...)` call

Impact:

- creating a new product now saves once instead of attempting duplicate insertion

### `FeaturedContentService.UpdateFeaturedContent` updated the wrong entity

File:

- `src/BestFit.Application/Services/FeaturedContentService.cs`

Fix:

- the service now updates and returns the incoming entity, not the previously loaded one

Impact:

- callers get the data they actually submitted

### `ApplicationDbContext` was using the wrong identity base type

File:

- `src/BestFit.Web/Data/ApplicationDbContext.cs`

Fix:

- switched from `IdentityDbContext` to `IdentityDbContext<ApplicationUser>`

Impact:

- the web app now aligns with the domain identity model used by views and services

### Payment form model validation was blocking successful checkout

File:

- `src/BestFit.Web/Models/Checkout/CheckoutPaymentViewModel.cs`

Fix:

- added `[ValidateNever]` to nested `Cart` and `Delivery` properties

Impact:

- posting the payment form now validates only the fields the form actually submits

## Running the Test Suite

## Prerequisites

- .NET SDK `8.0.416`

That version is pinned by `global.json`.

An additional build setting was added in `Directory.Build.props`:

- `BuildInParallel=false`

That setting was needed in this environment to avoid unreliable restore and graph-build behavior.

## Standard commands

Run unit tests:

```powershell
dotnet test tests\BestFit.UnitTests\BestFit.UnitTests.csproj -c Debug
```

Run integration tests:

```powershell
dotnet test tests\BestFit.IntegrationTests\BestFit.IntegrationTests.csproj -c Debug
```

Run acceptance tests:

```powershell
dotnet test tests\BestFit.AcceptanceTests\BestFit.AcceptanceTests.csproj -c Debug
```

## Coverage commands

Unit coverage:

```powershell
dotnet test tests\BestFit.UnitTests\BestFit.UnitTests.csproj `
  -c Debug `
  --collect:"XPlat Code Coverage" `
  --results-directory TestResults\CoverageFinal2\Unit `
  -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=cobertura
```

Integration coverage:

```powershell
dotnet test tests\BestFit.IntegrationTests\BestFit.IntegrationTests.csproj `
  -c Debug `
  --collect:"XPlat Code Coverage" `
  --results-directory TestResults\CoverageFinal\Integration `
  -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=cobertura
```

Acceptance coverage:

```powershell
dotnet test tests\BestFit.AcceptanceTests\BestFit.AcceptanceTests.csproj `
  -c Debug `
  --collect:"XPlat Code Coverage" `
  --results-directory TestResults\CoverageFinal\Acceptance `
  -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=cobertura
```

Each run writes a `coverage.cobertura.xml` file under a GUID-named subdirectory inside the chosen results directory.

## Coverage Files from the Latest Successful Run

These were the latest files generated during this work:

- `TestResults/CoverageFinal2/Unit/.../coverage.cobertura.xml`
- `TestResults/CoverageFinal/Integration/.../coverage.cobertura.xml`
- `TestResults/CoverageFinal/Acceptance/.../coverage.cobertura.xml`

Avoid hardcoding the GUID directory names in scripts because they change every run.

## Coverage Interpretation

The current coverage numbers are best understood by package:

| Package | Combined Line Coverage |
| --- | --- |
| `BestFit.Web` | 43.31% |
| `BestFit.Application` | 42.30% |
| `BestFit.Shared` | 25.60% |
| `BestFit.Domain` | 23.97% |
| `BestFit.API` | 8.58% |
| `BestFit.Infrastructure` | 0.00% |

The strongest coverage is in the web and application layers around checkout.

The weakest coverage is in:

- EF Core infrastructure
- most API CRUD endpoints
- account/auth flows
- profile flows
- image and measurement-profile paths

## Why Raw Coverage Looks Low

The coverage collector instruments a lot of code the team is not actively editing during this feature, especially:

- EF Core migration code
- generated designer code
- broad DTO and value-object surfaces
- startup code paths that are hard to hit incidentally

That is why:

- the all-in percentage is low
- the checkout and cart areas can still be well-tested in practice

For engineering decisions, the package-level numbers and file-level gaps are more useful than the raw repo-wide percentage.

## How to Add New Tests

## When to choose a unit test

Choose a unit test when:

- one service or controller owns the behavior
- dependencies can be replaced with in-memory doubles
- you care about validation, mapping, branching, or state transitions

Examples:

- cart mutation rules
- API request mapping inside a service
- controller redirects and temp data behavior

## When to choose an integration test

Choose an integration test when:

- two or more real classes must collaborate
- a unit test would mostly re-implement framework plumbing
- you want to verify real DTOs, AutoMapper, controller actions, and service collaboration

Examples:

- API controller to application service interaction
- checkout controller to web services interaction

## When to choose an acceptance test

Choose an acceptance test when:

- the important question is "does the user flow work?"
- redirects, routing, forms, anti-forgery, cookies, or session matter
- the feature crosses multiple MVC layers

Examples:

- cart to login gate to delivery to payment to receipt

## Preferred patterns

### For unit tests

- prefer the existing in-memory repositories and fake unit of work
- prefer `StubHttpMessageHandler` over ad hoc mocking frameworks
- keep assertions on observable behavior, not private implementation details

### For integration tests

- use the real controller and the real service
- fake the repositories only when the database is not the subject under test
- keep each test focused on one collaboration path

### For acceptance tests

- keep flows user-facing and high value
- avoid asserting brittle markup details
- prefer checking status codes, redirects, visible text, and persisted request payloads
- if a form changes, update the field names in the test rather than bypassing the form post

## Maintenance Rules for Test Hooks

If you add more test-only behavior in the web app, follow these rules:

1. Keep test hooks opt-in through configuration.
2. Keep production defaults unchanged.
3. Prefer configuration gates over `#if DEBUG`.
4. Keep test hooks narrow and specific to the problem they solve.
5. Document the hook in this file.

Good examples from this work:

- configurable API base URL
- test-only header auth
- test-only data-protection directory
- test-only HTTPS redirection bypass

## Known Limitations

The current suite is useful, but not complete.

### Infrastructure is still uncovered

What is missing:

- `BestFitDbContext`
- repository implementations
- entity configuration behavior

What would be needed to close the gap:

- a stable ephemeral database strategy
- SQL-backed repository tests
- possibly Testcontainers or a reliable local SQL setup

### Most API controllers are still uncovered

Current API coverage focuses on order checkout.

Still largely uncovered:

- category CRUD
- product CRUD
- carts
- order details
- product images
- measurement profiles
- featured content

### Account and profile flows are still largely uncovered

Current acceptance tests use header-based auth and do not walk the full login/register UI.

Still largely uncovered:

- registration
- real login/logout behavior
- profile pages
- account controller error handling

## Troubleshooting

## Acceptance tests hang during startup

Check:

- `TestAuthentication__Enabled=true`
- `TestHost__Enabled=true`
- `TestHost__DisableHttpsRedirection=true`
- `TestHost__DataProtectionDirectory` points to a writable location
- `ApiUrl` points to the stub server URI

## Payment acceptance tests render the page instead of redirecting

Most likely cause:

- a validation issue on `CheckoutPaymentViewModel`

Relevant file:

- `src/BestFit.Web/Models/Checkout/CheckoutPaymentViewModel.cs`

The current setup depends on `Cart` and `Delivery` being server-populated and marked with `[ValidateNever]`.

## NuGet restore fails or is flaky

This environment previously experienced TLS and remote-source failures during restore.

If that happens again, restore against local/offline feeds first and confirm the SDK from `global.json` is installed.

## Coverage collector produces no useful output

Use:

- `--collect:"XPlat Code Coverage"`

Do not rely on the legacy Windows profiler-based `Code Coverage` collector for day-to-day work.

The repo now uses `coverlet.collector` because it is more stable in this environment and outputs Cobertura XML directly.

## Recommended Backlog for the Next Developer

If the goal is to keep raising confidence and coverage, this is the highest-value order:

1. Add repository and `DbContext` tests for `BestFit.Infrastructure`.
2. Add controller tests for `AccountController` and `ProfileController`.
3. Add API controller tests for category, product, and cart endpoints.
4. Add tests for `AuthService` and `ProductImageService`.
5. Add a real login/register acceptance path once the identity environment is stable.

## Quick Summary

The current test system gives the team a maintainable base:

- fast unit tests for business and controller logic
- composition-level integration tests for class collaboration
- end-to-end acceptance tests for the checkout journey
- repeatable Cobertura coverage output

The highest-confidence area is checkout and order placement.
The biggest remaining gap is infrastructure and non-checkout API coverage.

If you are onboarding into this code, start in the unit tests, then read the integration harness, then run the acceptance flow. That path will give you the shortest route to understanding how the tested checkout system works and how to extend it safely.
