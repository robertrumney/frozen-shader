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
enemy.gameObject.GetComponent<EnemyAI>().enabled=false;
enemy.gameObject.GetComponent<Animation>().enabled=false;
AudioSource.PlayOneShot(freezeSound, enemy.transform.position, 0.5f);
```

When you play the scene, you should see the GameObject gradually transition to a frozen state over `timeToFreeze` seconds.

## Example

To see an example of the script and shader in action, attach the `FreezeObject` script to a GameObject with the ice shader material, then press play in Unity. The object should start to appear as if it's being frozen over time.

## Note

This shader and script are quite basic and might not work well for every game or scene. They're intended to provide a starting point and will likely need to be adjusted to suit your specific needs.

Feel free to modify and improve upon them as you see fit!
