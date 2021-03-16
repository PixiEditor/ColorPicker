[![Download](https://img.shields.io/badge/nuget-download-blue)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)
[![Downloads](https://img.shields.io/nuget/dt/PixiEditor.ColorPicker)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)

# About

A collection of WPF/.NET 5 controls the let users choose colors in various ways. 
Originally developed for [PixiEditor](https://github.com/PixiEditor/PixiEditor).

![screenshot](https://i.imgur.com/C6m5YWI.png)

1. [Included Controls](#controls)
1. [Example Usage](#example)
1. [Properties](#properties)
1. [Styling](#styling)
1. [Other](#other)

# Included Controls<a name="controls">

- `HSVPicker`: A HSV Color Picker, consisting of a circular hue slider and HV/HL square.
- `ColorSliders`: A set of HSV/RGB + Alpha sliders
- `HexColorTextBox`: An RGBA Hex text field
- `ColorDisplay`: A Primary/Secondary Color display with a swap button

**Additionally, two more controls are included for convenience**

- `StandardColorPicker` which combines everything listed above in one control
- `PortableColorPicker`, a collapsible version of StandardColorPicker

# Example Usage<a name="example">

See [ColorPickerDemo](https://github.com/PixiEditor/ColorPicker/tree/master/ColorPickerDemo) for an example project.

**Basic usage:**

Install the NuGet package, insert a reference to ColorPicker namespace
```
<Window ...
xmlns:colorpicker="clr-namespace:ColorPicker;assembly=ColorPicker"
...>
```
Add the controls
```
<colorpicker:StandardColorPicker x:Name="main" />
<colorpicker:PortableColorPicker ColorState="{Binding ElementName=main, Path=ColorState, Mode=TwoWay}"/>
```


# Properties<a name="properties">

Each control is inherited from PickerControlBase class and shares these common properies:

- `ColorState` Dependency Property, it contains all info about the current state of the control. Use this property to bind controls together.
- `Color`, which contains nested properties you may bind to or use to retrive the color in code-behind:
    - `Color.RGBAColor`: Current color as System.Windows.Media.Color
    - `Color.A`: Current Alpha, a double ranging from 0 to 255
    - `Color.RGB_R`, `Color.RGB_G`, `Color.RGB_B`: Dimensions of the RGB color space, each is a 0-255 double
    - `Color.HSV_H`: Hue in HSV color space, a 0-360 double 
    - `Color.HSV_S`: Saturation in HSV color space, a 0-100 double
    - `Color.HSV_V`: Value in HSV color space, a 0-100 double

Apart from those, some controls have unique properties:

- `SecondColorState` and `SecondColor` are functionally identical to `ColorState` and `Color`. 
Those are present on controls that have a secondary color.
- `SmallChange` lets you change SmallChange of sliders, which is used as sensitivity for when the user
turns scroll wheel with the curson over the sliders. Present on controls that contain color sliders 
(excluding circular hue slider).
- `ShowAlpha` lets you hide the alpha channel on various controls. 
Present on all controls containing either an alpha slider or a hex color textbox.

# Styling<a name="styling">

Out of the box, the color picker uses the default WPF look:

![Default ColorPicker look](https://i.imgur.com/N2sSQ9X.png)

You may use the included dark theme by loading a resource dictionary:
```
<Window.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/ColorPicker;component/Styles/DefaultColorPickerStyle.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Window.Resources>
```
and referencing DefaultColorPickerStyle in the style attribute of a control:
```
<colorpicker:StandardColorPicker Style="{StaticResource DefaultColorPickerStyle}" />
```
You may define your own styles, see 
[DefaultColorPickerStyle](https://github.com/PixiEditor/ColorPicker/blob/master/src/ColorPicker/Styles/DefaultColorPickerStyle.xaml) 
for reference.

# Other<a name="other">

Read flabbet's article on the theory behind the first version of this project on [dev.to](https://dev.to/flabbet/how-does-color-pickers-work-1275)