### Cloth Simulation


This is a sample simulation of Radius2D,

This Cloth Simulation is made by having few Circles ( with radius 0 ) being placed and connected with springs. Which is why it is not a normal cloth simulation but an Elastic Cloth Simulation.

It also have Mouse inputs for Simulating Wind Effects,

- Hold Left Mouse Button for Wind to blow from Left.
- Hold Right Mouse Button for Wind to blow from Right.

<p align="center">
    <a href="https://github.com/MIJGames/radius2D/Examples/ClothSimulation">
        <img src="../../Images/Cloth_Sim.png" width="65%" alt="Cloth Simulation">
    </a>
</p>

### Tweaking the Properties

To Tweak the Properties of the Cloth Simulation you can change the value of few variables and notice the effect on the simulation.

`numOfCircs` is an integer for the Square Root of Number of Circles in the Piece of Cloth <em>(Number of Circles in the Simulation will be equal to Square of this Number).</em>

`gapBetweenCircs` is a float for the Horizontal Gap Between the Circles.

`verticalGap` is a float for value of number to be multiplied with Horizontal Gap and result in Vertical Gap between the Circles.

`springStrength` is float for the overall Strength of the Springs connecting the Circles. It's orignal value will be used as Stiffness of Springs, and one-third of it will be used as Damping Factor of those Springs.