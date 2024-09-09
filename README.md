[![Discord Server](https://badgen.net/badge/discord/join%20chat/7289DA?icon=discord)](https://discord.gg/qSRMYmq)
[![Download](https://img.shields.io/badge/nuget-download-blue)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)
[![Downloads](https://img.shields.io/nuget/dt/PixiEditor.ColorPicker)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)

# About

A collection of various WPF and AvaloniaUI controls used to select colors.
Supports .NET Framework 4.5.1+, .NET Core 3.1+, .NET 5 - 7 (WPF) and .NET 6 - 8 (AvaloniaUI).
Originally developed for [PixiEditor](https://github.com/PixiEditor/PixiEditor).

![screenshot](https://i.imgur.com/4ysN4Fe.png)

# Included Controls

- `SquarePicker`: A HSV/HSL Color Picker, consists of a circular hue slider and HV/HL square.
- `ColorSliders`: A set of HSV/RGB + Alpha sliders
- `HexColorTextBox`: An RGBA Hex text field
- `ColorDisplay`: A Primary/Secondary Color display with a swap button
- `StandardColorPicker`: Combines everything listed above in one control
- `PortableColorPicker`: A collapsible version of StandardColorPicker
- `AlphaSlider`: A separate alpha slider control

![demo project](https://i.imgur.com/wZkkykY.png)

# Example Usage

See [ColorPickerDemo](https://github.com/PixiEditor/ColorPicker/tree/master/ColorPickerDemo) for an example project.

**Basic usage:**

- [WPF Basic Usage](https://github.com/PixiEditor/ColorPicker/tree/master/src/ColorPicker/README.md#example-usage)
- [AvaloniaUI Basic Usage](https://github.com/PixiEditor/ColorPicker/tree/master/src/ColorPicker.AvaloniaUI/README.md#example-usage)

# Properties

All controls share these properties:

- `SelectedColor` dependency property stores the current color as `System.Windows.Media.Color` or `Avalonia.Media.Color`. **Use this one if you simply want to get (or bind to) the current color.** When connecting controls together with bindings, use the `ColorState` dependency property instead.
- `ColorChanged`: An event that fires on SelectedColor change.
- `Color` property contains nested properties you may bind to or use to retrieve the color in code-behind:
  - `Color.A`: Current Alpha, a double ranging from 0 to 255
  - `Color.RGB_R`, `Color.RGB_G`, `Color.RGB_B`: Dimensions of the RGB color space, each is a 0-255 double
  - `Color.HSV_H`: Hue in the HSV color space, a 0-360 double
  - `Color.HSV_S`: Saturation in the HSV color space, a 0-100 double
  - `Color.HSV_V`: Value in the HSV color space, a 0-100 double
  - `Color.HSL_H`: Hue in the HSL color space, a 0-360 double
  - `Color.HSL_S`: Saturation in the HSL color space, a 0-100 double
  - `Color.HSL_L`: Lightness in the HSL color space, a 0-100 double
- `ColorState` dependency property contains all info about the current state of the control. Use this property to bind controls together. **Do not use it for any other purpose, use the other properties listed above.**

Apart from those, some controls have unique properties:

- `SecondColorState`, `SecondColor`, and `SecondaryColor` are functionally identical to `ColorState`, `Color`, and `SelectedColor` respectively. These are present on controls that have a secondary color.
- `HintColorState`, `HintNotifyableColor`, and `HintColor` are functionally identical to `ColorState`, `Color`, and `SelectedColor` respectively. These are present on controls that have a hint color. The hint color is a color field that can be used to obtain the primary color from an external source when the user clicks a button.
- `UseHintColor` enables the hint color or disables it (disabled by default).
- `SmallChange` lets you change `SmallChange` of sliders, which is used as sensitivity for when the user turns the scroll wheel with the cursor over the sliders. Present on controls with sliders.
- `ShowAlpha` lets you hide the alpha channel on various controls. Present on all controls containing either an alpha slider or a hex color textbox.
- `ShowFractionalPart` lets you hide the digits after the "." in the textboxes showing HSV and HSL values. Present on `ColorSliders` and on other controls containing `ColorSliders`.
- `PickerType`: HSV or HSL, present on `SquarePicker` and on controls that contain `SquarePicker`.
- `HexRepresentation`: RGBA or ARGB, present on `HexColorTextBox` and on controls that contain `HexColorTextBox`.

# Styling

Styling differs between AvaloniaUI and WPF version. See the respective READMEs for more info.

## AvaloniaUI

See [AvaloniaUI Styling](https://github.com/PixiEditor/ColorPicker/tree/master/src/ColorPicker.AvaloniaUI/README.md#styling)

## WPF

See [WPF Styling](https://github.com/PixiEditor/ColorPicker/tree/master/src/ColorPicker/README.md#styling)

# Other

Read flabbet's article on the theory behind the first version of this project
on [dev.to](https://dev.to/flabbet/how-does-color-pickers-work-1275)
