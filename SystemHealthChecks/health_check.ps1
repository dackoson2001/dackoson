# health_check.ps1

# Get the current date and time
$timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
$htmlFile = "C:\Users\$env:USERNAME\Desktop\health_check_report.html"

# HTML Structure
$htmlContent = @"
<html>
<head><style>
  table { border-collapse: collapse; width: 100%; }
  table, th, td { border: 1px solid black; }
  th, td { padding: 8px; text-align: left; }
  body { font-family: Arial, sans-serif; }
</style></head>
<body>
<h1>System Health Check Report</h1>
<p>Generated on: $timestamp</p>
<table>
  <tr><th>Metric</th><th>Value</th></tr>
"@

# Check CPU Usage
$cpuUsage = Get-WmiObject Win32_Processor | Select-Object -ExpandProperty LoadPercentage
$htmlContent += "<tr><td>CPU Usage</td><td>$cpuUsage%</td></tr>"

# Check Memory Usage
$memory = Get-WmiObject Win32_OperatingSystem
$memoryUsed = [math]::round(($memory.TotalVisibleMemorySize - $memory.FreePhysicalMemory) / 1MB, 2)
$memoryTotal = [math]::round($memory.TotalVisibleMemorySize / 1MB, 2)
$memoryUsage = [math]::round(($memoryUsed / $memoryTotal) * 100, 2)
$htmlContent += "<tr><td>Memory Usage</td><td>$memoryUsed MB / $memoryTotal MB ($memoryUsage%)</td></tr>"

# Check Disk Space Usage
$diskSpace = Get-WmiObject Win32_LogicalDisk | Where-Object { $_.DriveType -eq 3 } # 3 = Local Disk
foreach ($disk in $diskSpace) {
    $freeSpace = [math]::round($disk.FreeSpace / 1GB, 2)
    $totalSpace = [math]::round($disk.Size / 1GB, 2)
    $usedSpace = [math]::round(($disk.Size - $disk.FreeSpace) / 1GB, 2)
    $diskUsage = [math]::round(($usedSpace / $totalSpace) * 100, 2)
    $htmlContent += "<tr><td>Disk Usage ($($disk.DeviceID))</td><td>$usedSpace GB / $totalSpace GB ($diskUsage%) free: $freeSpace GB</td></tr>"
}

# Check Network Status
$networkStatus = Test-Connection -ComputerName google.com -Count 1 -Quiet
$networkStatus = if ($networkStatus) { "Connected" } else { "Disconnected" }
$htmlContent += "<tr><td>Network Status</td><td>$networkStatus</td></tr>"

# Close the HTML Structure
$htmlContent += "</table></body></html>"

# Save the HTML report
$htmlContent | Out-File -FilePath $htmlFile

Write-Host "Health Check HTML Report generated at: $htmlFile"
# Function to send an email alert
function Send-EmailAlert {
    param(
        [string]$subject,
        [string]$body
    )

    $smtpServer = "smtp.gmail.com"
    $smtpFrom = "your_email@gmail.com" # Replace with your email
    $smtpTo = "recipient_email@gmail.com" # Replace with recipient's email
    $smtpUser = "your_email@gmail.com" # Replace with your email
    $smtpPass = "your_password" # Replace with your password (or use an app-specific password)

    $SMTP = New-Object Net.Mail.SmtpClient($smtpServer, 587)
    $SMTP.Credentials = New-Object System.Net.NetworkCredential($smtpUser, $smtpPass)
    $SMTP.EnableSsl = $true

    $mailmessage = New-Object System.Net.Mail.MailMessage($smtpFrom, $smtpTo, $subject, $body)
    $SMTP.Send($mailmessage)
}

# Check CPU Usage and send an email alert if it's over 90%
$cpuUsage = Get-WmiObject Win32_Processor | Select-Object -ExpandProperty LoadPercentage
if ($cpuUsage -gt 90) {
    Send-EmailAlert -subject "Critical CPU Usage Alert" -body "CPU usage is at $cpuUsage%, which is above the critical threshold of 90%."
}

# Check Memory Usage and send an email alert if it's over 80%
$memory = Get-WmiObject Win32_OperatingSystem
$memoryUsed = [math]::round(($memory.TotalVisibleMemorySize - $memory.FreePhysicalMemory) / 1MB, 2)
$memoryTotal = [math]::round($memory.TotalVisibleMemorySize / 1MB, 2)
$memoryUsage = [math]::round(($memoryUsed / $memoryTotal) * 100, 2)
if ($memoryUsage -gt 80) {
    Send-EmailAlert -subject "Critical Memory Usage Alert" -body "Memory usage is at $memoryUsage%, which is above the critical threshold of 80%."
}
