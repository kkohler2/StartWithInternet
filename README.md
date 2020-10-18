# StartWithInternet
Utility to ensure public internet connection is available before running applications.

To configure apps, edit appsettings.json and just add the fully pathed out applications and any parameters.

Example:

{
  "Executables": {
    "Programs": [
      {
        "Name": "C:\\Util\\Bginfo64.exe",
        "Parameters": "/timer:0"
      },    
    ]
  }
}

On startup, run StartWithInternet instead of the programs that require internet connection.  Then the configured apps will be started once an external internet connection is established.
