# Goals

## Get it building
- [x] Update build.cake
- [x] Does it build locally?
- [x] Move away from GitVersion (sorry Jake)
- [ ] Move to GitVersion.NerdBank
    - [ ] Update the msbuild targets
    - [ ] Copy scripts/setversion.csproj

- [ ] Remove AppVeyor references

## Move to .NET Foundation CI
- [ ] Create CI pipeline
- [ ] Create CI PR pipeline
- [ ] Create deployment pipeline
- [ ] Is patch version v4.0.[1..3]?
- [ ] Release Splat v4.0.x

# Non goals
- Splat 4.1.0 is toast, can't reuse the package beause it wasn't signed
