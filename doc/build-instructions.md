# Build instructions

Detailed Build instructions with all available target frameworks, runtimes, configurations and example commands for building and publishind each project in the solution.

## Build for desktop platforms

### Build for Windows

Build project:

```pwsh
dotnet build src\ImeSense.Boilerplates.Avalonia.Windows\ImeSense.Boilerplates.Avalonia.Windows.csproj --configuration <Configuration> --framework <Framework>
```

Publish project with packing:

```pwsh
dotnet publish src\ImeSense.Boilerplates.Avalonia.Windows\ImeSense.Boilerplates.Avalonia.Windows.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime> --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true
```

Publish project without packing:

```pwsh
dotnet publish src\ImeSense.Boilerplates.Avalonia.Windows\ImeSense.Boilerplates.Avalonia.Windows.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime> --self-contained true -p:PublishSingleFile=false -p:PublishTrimmed=true -p:PublishReadyToRun=true
```

Supported values:

- `<Configuration>` - supported configurations:
  - `Debug`;
  - `Release`.
- `<Framework>` - used target framework:
  - `net8.0`.
- `<Runtime>` - supported runtimes:
  - `win-x86`;
  - `win-x64`;
  - `win-arm64`.

### Build for Linux

Build project:

```sh
dotnet build src/ImeSense.Boilerplates.Avalonia.Linux/ImeSense.Boilerplates.Avalonia.Linux.csproj --configuration <Configuration> --framework <Framework>
```

Publish project with packing:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.Linux/ImeSense.Boilerplates.Avalonia.Linux.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime> --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true
```

Publish project without packing:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.Linux/ImeSense.Boilerplates.Avalonia.Linux.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime> --self-contained true -p:PublishSingleFile=false -p:PublishTrimmed=true -p:PublishReadyToRun=true
```

Supported values:

- `<Configuration>` - supported configurations:
  - `Debug`;
  - `Release`.
- `<Framework>` - used target framework:
  - `net8.0`.
- `<Runtime>` - supported runtimes:
  - `linux-x64`;
  - `linux-arm`;
  - `linux-arm64`.

### Build for macOS

Build project:

```sh
dotnet build src/ImeSense.Boilerplates.Avalonia.macOS/ImeSense.Boilerplates.Avalonia.macOS.csproj --configuration <Configuration> --framework <Framework>
```

Publish project with packing:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.macOS/ImeSense.Boilerplates.Avalonia.macOS.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime> --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true
```

Publish project without packing:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.macOS/ImeSense.Boilerplates.Avalonia.macOS.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime> --self-contained true -p:PublishSingleFile=false -p:PublishTrimmed=true -p:PublishReadyToRun=true
```

- `<Configuration>` - supported configurations:
  - `Debug`;
  - `Release`.
- `<Framework>` - used target framework:
  - `net8.0`.
- `<Runtime>` - supported runtimes:
  - `osx-x64`;
  - `osx-arm64`.

## Build for web platforms

### Build for WebAssembly

Install workload:

```sh
dotnet workload install wasm-tools
```

Build project:

```sh
dotnet build src/ImeSense.Boilerplates.Avalonia.WebAssembly/ImeSense.Boilerplates.Avalonia.WebAssembly.csproj --configuration <Configuration> --framework <Framework>
```

Publish project:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.WebAssembly/ImeSense.Boilerplates.Avalonia.WebAssembly.csproj --configuration <Configuration> --framework <Framework>
```

- `<Configuration>` - supported configurations:
  - `Debug`;
  - `Release`.
- `<Framework>` - used target framework:
  - `net8.0-browser`.

## Build for mobile platforms

### Build for Android

Install workload:

```sh
dotnet workload install android
```

Install dependencies:

```sh
dotnet build src/ImeSense.Boilerplates.Avalonia.Android/ImeSense.Boilerplates.Avalonia.Android.csproj -t:InstallAndroidDependencies -f <Framework> -p:AndroidSdkDirectory=<AndroidSdkPath> -p:JavaSdkDirectory=<JdkPath> -p:AcceptAndroidSdkLicenses=True
```

Build project:

```sh
dotnet build src/ImeSense.Boilerplates.Avalonia.Android/ImeSense.Boilerplates.Avalonia.Android.csproj --configuration <Configuration> --framework <Framework> -p:AndroidSdkDirectory=<AndroidSdkPath> -p:JavaSdkDirectory=<JdkPath>
```

Publish project with all runtimes:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.Android/ImeSense.Boilerplates.Avalonia.Android.csproj --configuration <Configuration> --framework <Framework> -p:AndroidSdkDirectory=<AndroidSdkPath> -p:JavaSdkDirectory=<JdkPath>
```

Publish project with specified runtime:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.Android/ImeSense.Boilerplates.Avalonia.Android.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime> -p:AndroidSdkDirectory=<AndroidSdkPath> -p:JavaSdkDirectory=<JdkPath>
```

- `<Configuration>` - supported configurations:
  - `Debug`;
  - `Release`.
- `<Framework>` - used target framework:
  - `net8.0`.
- `<Runtime>` - supported runtimes:
  - `android-arm`;
  - `android-arm64`;
  - `android-x86`;
  - `android-x64`.
- `<AndroidSdkPath>` - path to Android SDK.
- `<JdkPath>` - path to JDK.

### Build for iOS

Install workload:

```sh
dotnet workload install ios maccatalyst
```

Install __Xcode 26.0__.

Install __iOS 26.0 + iOS 26.0.1 Simulator__ workload.

Select __Xcode 26.0__:

```sh
sudo xcode-select --switch /Applications/Xcode_26.0.app
```

Build project:

```sh
dotnet build src/ImeSense.Boilerplates.Avalonia.iOS/ImeSense.Boilerplates.Avalonia.iOS.csproj --configuration <Configuration> --framework <Framework>
```

Publish project:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.iOS/ImeSense.Boilerplates.Avalonia.iOS.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime>
```

Publish project with packing and signing:

```sh
dotnet publish src/ImeSense.Boilerplates.Avalonia.iOS/ImeSense.Boilerplates.Avalonia.iOS.csproj --configuration <Configuration> --framework <Framework> --runtime <Runtime> -p:ArchiveOnBuild=true -p:CodesignKey="Apple Distribution: <CodesignKey>" -p:CodesignProvision="<CodesignProvision>" -p:ServerAddress=<MacOsAddress> -p:ServerUser=<MacOsUsername> -p:ServerPassword=<MacOsPassword> -p:TcpPort=58181
```

- `<Configuration>` - supported configurations:
  - `Debug`;
  - `Release`.
- `<Framework>` - used target framework:
  - `net8.0-ios`.
- `<Runtime>` - supported runtimes:
  - `ios-arm64`;
  - `iossimulator-x64` (build only);
  - `iossimulator-arm64` (build only).
- `<CodesignKey>` - name of code signing key.
- `<CodesignProvision>` - provisioning profile to use for signing.
- `<MacOsAddress>` - macOS build host IP address.
- `<MacOsUsername>` - macOS username.
- `<MacOsPassword>` - macOS password.
