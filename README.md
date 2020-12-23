[![Download](https://img.shields.io/badge/nuget-download-blue)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)
[![Downloads](https://img.shields.io/nuget/dt/PixiEditor.ColorPicker)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)

# About

A WPF Color picker made for [PixiEditor](https://github.com/PixiEditor/PixiEditor). Available to use for anyone, anywhere!

![screenshot](https://i.imgur.com/C6m5YWI.png)

Read about the theory behind it on [dev.to](https://dev.to/flabbet/how-does-color-pickers-work-1275)

# Controls

There are two controls avaliable: `StandardColorPicker` (shown on the screenshot above) and `PortableColorPicker`. PortableColorPicker is an expandable version of StandardColorPicker.

# Usage

Install the NuGet package, insert a reference to ColorPicker namespace
```
<Window ...
xmlns:colorpicker="clr-namespace:ColorPicker;assembly=ColorPicker"
...>
```
and add either StandardColorPicker or PortableColorPicker controls
```
<StackPanel>
	<colorpicker:StandardColorPicker />
	<colorpicker:PortableColorPicker />
</StackPanel>
```
Currently selected and secondary colors are stored inside `SelectedColor` and `SecondaryColor` dependency properties.

# Styling

Out of the box, the color picker uses the default WPF look:

![Default ColorPicker look](https://i.imgur.com/N2sSQ9X.png)

You have an option to use the included dark theme by loading a resource dictionary:
```
<Window.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/ColorPicker;component/Styles/DefaultColorPickerStyle.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Window.Resources>
```
and referencing DefaultColorPickerStyle in the style attribute of the control:
```
<colorpicker:StandardColorPicker Style="{StaticResource DefaultColorPickerStyle}" />
```
You may also define your own styles, see [DefaultColorPickerStyle](https://github.com/PixiEditor/ColorPicker/blob/master/src/ColorPicker/Styles/DefaultColorPickerStyle.xaml) for reference.