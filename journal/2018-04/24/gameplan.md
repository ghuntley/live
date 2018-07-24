# Goals

## Get it building
- [x] Update build.cake
- [x] Does it build locally?
- [x] Move away from GitVersion (sorry Jake)
- [x] Move to GitVersion.NerdBank
    - [x] Update the msbuild targets
    - [x] Copy scripts/setversion.csproj

- [x] Remove AppVeyor references

## Move to .NET Foundation CI
- [x] Create CI pipeline
- [x] Create CI PR pipeline
- [x] Create deployment pipeline
- [ ] Put in the right username/password tokens for "reactiveuibot"@github
- [x] Is patch version v4.0.[1..3]?
- [x] Configure branch protection so folks can't push master, must PR
- [-] Open dodgy PR does it go RED?
- [ ] Verify is pushed to MyGet
- [ ] Push to NuGet and verify it goes live.

- [ ] Splat v4.0.x release announcement

# Non goals
- Splat 4.1.0 is toast, can't reuse the package beause it wasn't signed
