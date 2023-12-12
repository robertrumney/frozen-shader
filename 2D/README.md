# 2D Ice Shader for Unity

This folder contains a custom 2D Ice Shader for Unity, designed to add a dynamic and visually appealing ice effect to sprites. The shader features adjustable properties like refraction, translucency, and a frozen effect, making it versatile for different icy appearances.

## Features

- **Texture Support**: Applies the shader effect to any 2D sprite.
- **Normal Mapping**: Enhances the visual depth with bump mapping.
- **Refraction**: Simulates the light bending effect through ice.
- **Translucency and Subsurface Scattering**: Mimics the light scattering beneath the surface for a more realistic ice effect.
- **Dynamic Frozen Effect**: Control the intensity of the ice effect dynamically.

## Properties

- `_MainTex`: The main texture of the sprite.
- `_BumpMap`: Normal map for the bump effect.
- `_BumpScale`: Adjusts the scale of the normal map effect.
- `_Refraction`: Controls the refraction intensity.
- `_Translucency`: Adjusts the translucency of the ice.
- `_SubsurfaceColor`: Color of the subsurface scattering effect.
- `_FrozenAmount`: Controls the intensity of the frozen effect.

## Usage

Simply create a new material and assign it to a sprite, add an icey normal (try the one I've added in the resources folder) and adjust the colors and normal scale alongside translucency for the desired effect.
