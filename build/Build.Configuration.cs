﻿sealed partial class Build
{
    const string Version = "1.1.0";
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "output";
    readonly AbsolutePath ChangeLogPath = RootDirectory / "Changelog.md";

    protected override void OnBuildInitialized()
    {
        Configurations =
        [
            "Release*",
            "Installer*"
        ];

        Bundles =
        [
            Solution.RevitWebView2_Application
        ];

        InstallersMap = new()
        {
            { Solution.Build.Installer, Solution.RevitWebView2_Application }
        };
    }
}