# n-animation

The basic idea behind this package is that you want to run a series of minor
animations and effects, and, occasionally, wait until some of them are done
before you continue with other actions.

For example, you drag an object and want it smoothly 'snap back' into the
original position, and lock the UI until the animation is completed.

The other part of the motivation for this is that calling a million `Update()`
calls is actually significantly slower than calling a custom `UpdateMe()` function
a million times in a single `Update()` function. There's more background on that
here: http://blogs.unity3d.com/2015/12/23/1k-update-calls/

## Usage

Very simply to create an animation implement the `IAnimation` interface on a
behaviour and add it to an object. If the object is to be destroyed before the
target is, use the `AnimationManager.Spawn()` function to create a temporary
object.

You can then use the `AnimationManager` type to register handlers for various
callbacks like completion, stream completion, etc.

If you want a custom animation curve, simply implement a new `IAnimationCurve`.

See the tests in the `Editor/` folder for each class for usage examples.

The `tests` folder has a bunch of examples as well.

### Multitarget animations

Sometimes you want an animation such as a multi-target 'burst' animation that
scatters targets in all directions.

For this sort of animation, you still extend `IAnimation` for the primary animation,
but then call `AddChildren()` with either a specific `GameObject` or with some
type that extends `IAnimationChildSelector` that can resolve a source for animated
objects.

Notice specifically that *unlike* the main animation, no component is added to the
child objects to bind then to a specific animation; only the parent object has an
animation component attached to it.

A number of default selectors can be founder under `selectors`.

## Install

From your unity project folder:

    npm init
    npm install shadowmint/unity-n-animation --save
    echo Assets/packages >> .gitignore
    echo Assets/packages.meta >> .gitignore

The package and all its dependencies will be installed in
your Assets/packages folder.

## Development

Setup and run tests:

    npm install
    npm install ..
    cd test
    npm install
    gulp

Remember that changes made to the test folder are not saved to the package
unless they are copied back into the source folder.

To reinstall the files from the src folder, run `npm install ..` again.

### Tests

All tests are wrapped in `#if ...` blocks to prevent test spam.

You can enable tests in: Player settings > Other Settings > Scripting Define Symbols

The test key for this package is: N_ANIM_TESTS
