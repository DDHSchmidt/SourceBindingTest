# SourceBindingTest
Test project for issue with MAUI's new CollectionViewHandler2 for iOS

When using the new ColectionViewHandler2 from Maui 9, none of the attached properties can be evaluated anymore.
Don't get confused by the useless "PressedRoutingEffect", it is only a placeholder for other, more useful effects.

If you outcomment the "AddHandler" line in MauiProgram.cs, the attached properties can be evaluated again and the output in the bottom line of the screen will be updated on clicks.