#! /usr/local/bin/pwsh

# If the "Make" column is missing from a spreadsheet, this infers makes
# from other/"old" spreadsheet data.

param(
    # Path to input JSON with missing makes
    [Parameter(Mandatory)]
    [string]
    $InputPath,

    # Path to input JSON with makes
    [Parameter(Mandatory)]
    [string]
    $InputJSONWithMakes,

    # Path to output JSON with filled makes
    [Parameter(Mandatory)]
    [string]
    $OutputPath
)

$workingData = Get-Content -Path $InputPath | ConvertFrom-Json
$filledMakeData = Get-Content -Path $InputJSONWithMakes | ConvertFrom-Json

$makeLookup = @{}

foreach ($entry in $filledMakeData.saleEntries) {
    $makeLookup[$entry.Model] = $entry.Make
}

foreach ($entry in $workingData.saleEntries) {
    if ($null -eq $entry.Make) {
        $entry.Make = $makeLookup[$entry.Model]

        if ($null -eq $entry.Make) {
            Write-Host "No Make for $($entry.Model)"
        }
    }
}

$workingData | ConvertTo-Json -Depth 3 -Compress | Out-File -FilePath $OutputPath -Force