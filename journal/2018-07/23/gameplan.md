# Introduction

1. Show blog post and talk about succession.

    https://reactiveui.net/blog/2018/05/reactiveui-succession

2. Here today to release new version of reactiveui so others can do it instead of me.
3. Running people through the release process so my thoughts are archived.

    https://reactivex.slack.com/messages/CAK67UA7M/

4. Updating documentation so that people don't need to watch video.

# Explain how CI currently works

- We run a single branch of master

    https://github.com/reactiveui/ReactiveUI/settings/branches

- GitHub is configured to only allow merges if branch is in alignment with master (up to date)

    https://github.com/reactiveui/ReactiveUI/settings/branches/develop

- Two jobs

    https://dotnetfoundation.visualstudio.com/ > ReactiveUI > Builds

    - CI that validates master
    - CI PR that validates the PR against master

# Explain how CD currently works

- There's two environments (MyGet and NuGet)

    https://dotnetfoundation.visualstudio.com/ > ReactiveUI > Releases

    - MyGet is automatically deployed
    - NuGet requires manual approval

- The semver patch number is automatically incremented
- The major and minor require a pull request to update
- We roll forward always, thus if we make a mistake during release then send PR to master then deploy again as patch release

# What has changed?

> For my own interest what has changed since the last release (git tag)

    > git log --decorate

> Comment on the commit message hygene needs to improve
> The PR# should be in the commit message

# Visit github

- Ensure that version is incremented correctly
- Approve pull-request
- Merge pull-request

# VSTS 

- Part of dotnet foundation, they can grant new maintainers access. Speak with Oren

- A new build will be generated

https://dotnetfoundation.visualstudio.com/ReactiveUI/_build

- MyGet gets the release automatically so you can test further before pressing the button
- NuGet requires manaul approval, let's click the approve button

# Update release notes

- A tag is generated at GitHub with the version number
- Now we need to update the release notes


    __All Platforms__

    - Wouterdek submitted a patch for [ReactiveList that ensures that SuppressChangeNotifications() publishes the reset Changing notification before the changes are made, when the suppression is first enabled](https://github.com/reactiveui/ReactiveUI/commit/797ed8d226ccaf65c2012a7975d9cdba7697882d)

    - Dominik Mydlil contributed a patch that resolves conditions where [ReactiveList.AddRange would throw "Range actions are not supported" given a list of length 2<=n<=10, under certain circumstances](https://github.com/reactiveui/ReactiveUI/pull/1366)

    - Geoffrey Huntley contributed a patch that [bumps MSBuild.Extras which was mistakenly adding incorrect framework references](https://github.com/reactiveui/ReactiveUI/pull/1693)

    - Geoffrey Huntley contributed a patch that bumps Splat to v4.1.0 to resolve MSBuild.Extras which was mistakenly adding incorrect framework references](https://github.com/reactiveui/ReactiveUI/pull/1693)


    __House Keeping__

    - Colt contributed patch that [stamps the target framework version info assemblies thus you can now right click on an assembly to find out if that assembly is the `netstandard20` or `net45` dll](https://github.com/reactiveui/ReactiveUI/pull/1604)


    __Android__


    - Roman Vaughan contributed a patch that [adds a resolution strategy to fragments and WireUpControls](https://github.com/reactiveui/ReactiveUI/pull/1607)


    __Tizen__


    - Kangho Hur sent in a [PR that adds the Tizen platform to ReactiveUI. Thank-you!](https://github.com/reactiveui/ReactiveUI/pull/1546)


    __Windows Presentation Framework_



    __Windows Forms__


    - Sebastian Richter contributed a patch that resolves an issue where the [WinForms Designer would break when a child control uses WhenActivated](https://github.com/reactiveui/ReactiveUI/pull/1651)


    ### Where to get it

    You can download this release from [nuget.org](https://www.nuget.org/packages/reactiveui/8.4.1)

# Update the website

- Create blog post
- Show people how to update documentation.

# Wrap-up

- Thank the financial sponsors of reactiveui.

    https://reactiveui.net/

- Thank Glen and everyone else who helped in this release.