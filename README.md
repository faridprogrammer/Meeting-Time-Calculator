# Meeting Time Calculator

This program calculates the total time spent in meetings over a specified period using an `.ics` calendar file. It excludes events based on user-defined keywords from a configuration file (`appsettings.json`).

## Features

- Parses `.ics` calendar files.
- Calculates meeting durations for a specified time period (in days).
- Excludes specific events based on keywords defined in the configuration file.
- Supports flexible configurations via `appsettings.json`.

## Requirements

- [.NET 6](https://dotnet.microsoft.com/download) or later
- NuGet Packages:
  - `Ical.Net`
  - `Microsoft.Extensions.Configuration`
  - `Microsoft.Extensions.Configuration.Json`
  - `Microsoft.Extensions.Configuration.Binder`

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/meeting-time-spend-calculator.git
   cd meeting-time-spend-calculator
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

## Usage

Run the program from the command line, specifying the path to the `.ics` file and the time period (in days):

```bash
program.exe <path_to_ics_file> <time_period_in_days>
```

### Example:
```bash
program.exe "C:\calendar\my_calendar.ics" 30
```

This calculates the total time spent in meetings over the last 30 days.

## Configuration

### `appsettings.json`

The program uses an `appsettings.json` file to define keywords for excluding specific events. For example:

```json
{
  "ExcludedKeywords": [
    "ناهار",
    "break",
    "lunch"
  ]
}
```

Place this file in the same directory as the executable.

## Output

The program outputs the total meeting time (in hours) for the specified period, excluding events with matching keywords.

### Example Output:
```
Total time spent in meetings over the last 30 days (excluding specified keywords): 10.75 hours
```

