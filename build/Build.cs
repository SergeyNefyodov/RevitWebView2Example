using Nuke.Common.Git;
using Nuke.Common.ProjectModel;

sealed partial class Build : NukeBuild
{
    [GitRepository] readonly GitRepository GitRepository;
    Project[] Bundles;
    string[] Configurations;

    [Secret] [Parameter] string GitHubToken;
    Dictionary<Project, Project> InstallersMap;
    [Solution(GenerateProjects = true)] Solution Solution;

    public static int Main() => Execute<Build>(x => x.Compile);
}