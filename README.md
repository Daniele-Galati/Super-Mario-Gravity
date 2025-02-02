# Super-Mario-Gravity
 
## How does it work
The system is made up of two main components: the gravity field and the gravity agent.

### Gravity Fields
The first one is a class with 4 sub-classes:
* Spherical field - simulates planet gravity
* Cylindrical field - the gravity center is a line, thus the force lies in the plane containing the agent perpendicular to the cylinder axis
* Directional field - the gravity pulls the agents in one pre-determined direction
* Transition field - useful for zones where there is a curvature between two directional fields (you can see it in action in the demo scene)

Each field is color coded differently in the scene to understand immediately what type they are.
If two fields are colliding with each other, in the inspector it is possible to sort them by priority (the agents will be attracted from the highest priority field).

### Gravity Agents
The gravity agent is an object that is subject to gravity fields.
It automatically orients itself to stand up on the underneath plane normal or, in case the field is cylindrical/directional, its orientation depends solely on the gravity direction.

In the demo scene is already implemented a player that uses the gravity agent script, but it can be used on any game object with a rigidbody attached.
