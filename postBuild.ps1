$source=$args[0]
$destination=$args[1]
$projectName=$args[2]
$configurationName = $args[3]
$gDriveParentFolderId=$args[4]
$projectPath=$args[5]
$uploadToDrive=$args[6]


$sourceMinusLastChar = $source.Substring(0,$source.Length-1)
$sourceFolderName = $sourceMinusLastChar.Substring($sourceMinusLastChar.LastIndexOf('\')+1)
$renamedDestinationFolder = $destination.Substring(0,$destination.LastIndexOf('\')) + '\' + $projectName
$initialCopiedFolderName = $destination + $sourceFolderName
$zipFileName = $renamedDestinationFolder +'.zip'

if (Test-Path -path $zipFileName)
{
    Remove-Item -Recurse -Force $zipFileName
} 

if (!(Test-Path -path $destination)) 
{
    New-Item $destination -Type Directory
}

Copy-Item -Path $source -Destination $destination -recurse -Force

Rename-Item $initialCopiedFolderName  $renamedDestinationFolder

Compress-Archive $renamedDestinationFolder $zipFileName

if (($configurationName -eq 'Release') -and ($uploadToDrive -is [String]) -and ($uploadToDrive.ToLower() -eq 'true'))
{
    $expression = "cd "+$projectPath
    Invoke-Expression $expression
    $expression = "node postBuild.js " + $projectName + " " + $gDriveParentFolderId + " " + $zipFileName
    Invoke-Expression $expression
}

Remove-Item -Recurse -Force $renamedDestinationFolder