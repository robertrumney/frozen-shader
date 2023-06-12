# Unity Ice Shader

This project provides a custom Unity shader that can make objects appear as if they're encased in ice. The ice effect can be animated, allowing objects to transition smoothly between their normal appearance and the frozen state.

## Shader

The shader uses albedo and normal maps as a base but includes additional properties to adjust the ice effect. It takes into consideration properties like Fresnel reflection, transparency, specular highlights and reflectivity to provide a more realistic ice appearance.

## Script

The accompanying `FreezeObject.cs` script is responsible for gradually changing an object's appearance to a frozen state. It adjusts the `_FrozenAmount` property of the shader over time to create the transition.

## Usage

### Adding the Shader

1. Import shader into project
2. Create a new Material (Right click in Project panel -> Create -> Material).
3. In the Inspector, set the Shader of this material to the new ice shader (should be under Custom -> IceShader).
4. Adjust the shader properties as needed. For instance, you can change the `_FrozenAmount`, `_IceColor`, `_Transparency`, and `_Glossiness` to suit your needs.

### Example Usage

```csharp
// Simply attach this component to any object that contains a renderer to begin freezing it.
enemy.gameObject.AddComponent<FreezeObject>();

// Example logic for "Freezing" enemy
enemy.gameObject.GetComponent<EnemyAI>().enabled = false;
enemy.gameObject.GetComponent<Animation>().speed = 0;
AudioSource.PlayClipAtPoint(freezeSound, enemy.transform.position, 0.5f);
```

When you play the scene, you should see the GameObject gradually transition to a frozen state over `timeToFreeze` seconds.

## Important Info

It is very important to note, that you should never add the `FreezeObject` script to an object more than once. A flag of some kind is useful:
```csharp
if(!enemy.isFrozen)
{
    enemy.gameObject.AddComponent<FreezeObject>();
    enemy.isFrozen=true;
}
```

It is also VERY important to note that shader MUST be installed into a `/Resources/` folder in order for the `FreezeObject` script to function. If it is not, the shader will be pink.

## Example
To watch this shader in action within the context of an actual game [WATCH HERE](https://www.youtube.com/watch?v=7NfaKEPl7p0)

## Note

This shader and script are quite basic and might not work well for every game or scene. They're intended to provide a starting point and will likely need to be adjusted to suit your specific needs.

Feel free to modify and improve upon them as you see fit!
