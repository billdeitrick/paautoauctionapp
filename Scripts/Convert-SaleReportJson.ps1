#! /usr/local/bin/pwsh

# Converts a sale report to JSON to post to the API

param(
    # Path to input CSV
    [Parameter(Mandatory)]
    [string]
    $InputPath,

    # Path to output JSON
    [Parameter(Mandatory)]
    [string]
    $OutputPath,

    # Sale date
    [Parameter(Mandatory)]
    [datetime]
    $SaleDate
)

$in = Import-Csv -Path $InputPath

$output = [PSCustomObject]@{
    saleDate    = "$($SaleDate.Year)-$($SaleDate.Month.ToString("00"))-$($SaleDate.Day.ToString("00"))"
    saleEntries = New-Object -TypeName System.Collections.Generic.List[PSCustomObject]
}

foreach ($row in $in) {

    $cleanedPrice = $row.'Sale Price'.Replace(',', '').Replace('$', '').Trim()
    $cleanedPriceInt = $null

    if ([double]::TryParse($cleanedPrice, [ref]$cleanedPriceInt)) {
        $entry = [PSCustomObject]@{
            itemNumber = [int]$row.'Item #'
            year       = [int]$row.Year
            make       = $row.Make
            model      = $row.Model
            mileage    = [int]$row.Mileage
            deptNumber = [int]$row.'Dept #'
            saleNumber = [int]$row.'Sale #'
            comments   = $row.Comments
            salePrice  = [int]$cleanedPriceInt
        }

        $output.saleEntries.Add($entry)
    }

}

$output.saleEntries = $output.saleEntries.ToArray()

$output | ConvertTo-Json -Depth 2 -Compress | Out-File -Path $OutputPath