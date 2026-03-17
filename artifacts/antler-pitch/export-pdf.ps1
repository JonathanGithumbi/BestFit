param(
    [string]$HtmlPath = "$PSScriptRoot\BestFit-Antler-PitchDeck.html",
    [string]$OutputPath = "$PSScriptRoot\BestFit-Antler-PitchDeck.pdf"
)

$edgePath = "C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"

if (-not (Test-Path $edgePath)) {
    throw "Microsoft Edge was not found at $edgePath"
}

$resolvedHtml = (Resolve-Path $HtmlPath).Path.Replace('\', '/')
$resolvedOutput = (Resolve-Path (Split-Path $OutputPath -Parent)).Path
$resolvedOutput = (Join-Path $resolvedOutput (Split-Path $OutputPath -Leaf))
$fileUrl = "file:///$resolvedHtml"

& $edgePath `
    --headless=new `
    --disable-gpu `
    --allow-file-access-from-files `
    --print-to-pdf="$resolvedOutput" `
    --print-to-pdf-no-header `
    $fileUrl | Out-Null

if (-not (Test-Path $resolvedOutput)) {
    throw "PDF export did not produce $resolvedOutput"
}

Write-Output "Exported PDF to $resolvedOutput"
