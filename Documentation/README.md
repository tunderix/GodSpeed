# Welcome to GodSpeed - Ioni Essentials

This is a unity package holding essential components, scripts and helpers used in Ioni's personal unity projects.
For now the package offers:

- Debug Wrapper
- Singleton design pattern
- Custom attribute based dependency injection system
- Commonly used extension methods
- GameEvent pattern using scriptable object
- Helper utilities

# Using the systems

## Game Event System using Scriptable Object (SO)

The game event system is a common listener pattern. The flow goes as follows:

1. Create a GameEvent-SO in Unity.
2. Attach either a *GameEventListener* or *GameEventListenerWithDelay* into one of your MonoBehaviours, for example UI, to tell the system where you want to subscribe to the event.
3. Drag your **GameEvent-SO** into the listener and hook it up with a unity event by dragging it to "Act Event".
4. Drag your **GameEvent-SO** into somewhere you want to launch it from
5. Call the `Invoke()` method of the GameEvent

## Singleton

The singleton pattern found in the package is a generic pattern of singleton applied to MonoBehaviour.

1. Create a new class in form of `public class Injector : Singleton<Injector> {}`
   What is happening here is that you determine a MonoBehaviour, with only one instance in the scene.
2. Go into unity, and add the script to one of your MonoBehaviours
3. Configure fields: `debugging` and `preserveBetweenScenes`. Preserve betweens scenes means the object will be a Dont Destroy On Load object.

## Dependency Injection System

Dependency Injection is run by two attributes: `[Provide]` and `[Inject]`.
Injections works on

- Class
- Field
- Method
- Interface

!!! At the moment of typing this, there is no support for properties.

### Common flow:

1. Create **Injector**
   1.1. Create a new GameObject and call it something like Injector.
   1.2. Drag `Injector`-singleton from `Ioni.DependencyInjection` into your new Injector
   1.3. Create a new class that inherits from `IDependencyProvider`. Something like this for example:
   `public class Provider: MonoBehaviour, IDependencyProvider {}`
2. Provide a **service**
   2.1. Make a class you want to provide, example: `public class ServiceExample {}`
   2.2. Add a provider method with `[Provide]` -attribute into your Provider-class you created on 1.3.
   This methods name doesnt really matter, the important bit is the attribute.
   Example: `[Provide]  public ServiceExample ProvideServiceExample()  {  return new ServiceExample();  }`
3. **Injection** - Now you can inject this ServiceExample into your components and scripts.
   3.1. Open a script where you want to use this Service, and use the `[Inject]` -attribute to reference the system created.
   `[Inject] public ServiceExample exampleService;`

### Different approaches
There are multiple ways to use the injection system. One of the best ways IMO is to have an interface describing the system contents and provide like so:

    public interface IEnvironmentSystem  
	{  
		IEnvironmentSystem ProvideEnvironmentSystem();  
		string spawnAreas { get; }  
		void Initialize();  
	}
	public class EnvironmentSystem : MonoBehaviour, IDependencyProvider, IEnvironmentSystem {
	    [Provide]  
		public IEnvironmentSystem ProvideEnvironmentSystem() { return this; }
    }

And use this somewhere else like:
`[Inject] public IEnvironmentSystem environmentSystem;`