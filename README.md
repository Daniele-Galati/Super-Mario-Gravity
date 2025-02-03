# Super Mario Gravity
A simple system that replicates Super Mario Galaxy's gravitational fields

## How does it work
The system is made up of two main components: the gravity field and the gravity agent.

### Gravity Fields
The gravity field is a class with 4 sub-classes.
Each field is color coded differently in the scene to understand immediately what type they are.
* **Spherical field** (red) - simulates planet gravity
* **Cylindrical field** (blue) - the gravity center is a line, thus the force lies in the plane containing the agent perpendicular to the cylinder axis
* **Directional field** (green) - the gravity pulls the agents in one pre-determined direction
* **Transition field** (violet) - useful for zones where there is a curvature between two directional fields (you can see it in action in the demo scene)

If two fields are colliding with each other, in the inspector it is possible to sort them by priority (the agents will be attracted from the highest priority field).

P.S. The cylindrical field can be also used inside a _spline instantiate_ script to make the field follow a spline. A simple example can be found inside the Donut Planet prefab.

### Gravity Agents
The gravity agent is an object that is subject to gravity fields.
It automatically orients itself to stand up on the underneath plane normal or, in case the field is cylindrical/directional, its orientation depends solely on the gravity direction.

In the demo scene is already implemented a player that uses the gravity agent script, but it can be used on any game object with a rigidbody attached.

### Further improvements
The main issue is that the player can orbit too easily. A simple fix would be to introduce a suction force that keeps the player grounded. However, this force should deactivate when the player jumps to ensure smooth movement and prevent any interference.
