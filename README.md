# Walking Bot: AI-Powered Humanoid Balance Learning

[![Unity](https://img.shields.io/badge/Unity-2019.4+-blue.svg)](https://unity3d.com/)
[![C#](https://img.shields.io/badge/C%23-8.0+-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

An AI simulation that teaches a humanoid robot to stand upright using genetic algorithms and neural networks.

## Overview

This project demonstrates how artificial intelligence can learn complex motor skills through evolutionary computation. A 3D simulation of a bipedal humanoid robot is trained to maintain balance and stand upright using a genetic algorithm approach implemented in Unity.

### Key Features

- 3D Physics Simulation using Unity Game Engine
- Genetic Algorithm for evolutionary learning
- Neural Network Brain with 5 fully connected layers
- Real-time fitness evaluation based on balance and posture
- Population-based learning with 200 individuals per generation

## Technical Architecture

### Robot Model
The simulated humanoid consists of a spherical torso with two legs featuring knee joints, controlled by four actuators (two knee joints and two hip joints).

### Neural Network Architecture
Each robot is controlled by a feedforward neural network:
- **Input Layer**: 30 neurons (position and rotation data from 5 body parts)
- **Hidden Layers**: 25 → 20 → 16 → 8 neurons with sigmoid activation
- **Output Layer**: 4 neurons (torque commands for actuators)
- **Total Parameters**: 1,803 trainable weights

### Genetic Algorithm Implementation

1. **Population Initialization**: 200 robots with randomized neural network weights
2. **Physics Simulation**: Each robot attempts to maintain balance for a fixed duration
3. **Fitness Evaluation**: Scoring based on torso height, standing duration, and ground contact
4. **Selection**: Top 10 performers (elite) survive unchanged to next generation
5. **Reproduction**: Remaining 190 robots generated through crossover and mutation of elite genetics
6. **Iteration**: Process repeats across thousands of generations

### Fitness Function

The fitness evaluation incorporates:
- Squared vertical distance of torso from ground multiplied by time step
- Penalties for torso ground contact or loss of limb ground contact
- Rewards for sustained upright posture

## Results

After 4,318 generations of evolution, the system successfully trained robots to maintain stable bipedal balance for the entire simulation duration, demonstrating emergent learning of complex motor control behaviors.

## Implementation Details

### Core Components
- **DNA.cs**: Genetic encoding of neural network weights (1,803 genes)
- **Brain.cs**: Neural network forward propagation implementation
- **SimulationController.cs**: Main simulation orchestrator
- **FitnessEvaluator.cs**: Performance scoring system

### Configuration Parameters
- Population Size: 200 individuals
- Elite Count: 10 top performers preserved
- Mutation Rate: Configurable probability of weight mutation
- Simulation Duration: 10 seconds per evaluation

## Getting Started

### Prerequisites
- Unity 2019.4 or later
- Visual Studio or compatible C# IDE

### Installation
```bash
git clone https://github.com/Adithya-Jayan/EC460_WalkingBot.git
```

Open the project in Unity, load the main scene, and press Play to start the simulation.

## Future Development

- Extension to bipedal locomotion behaviors
- Terrain adaptation capabilities
- Transfer learning to physical robot hardware
- Implementation of advanced neural architectures
- Multi-objective optimization for speed and efficiency

## Academic Context

**Course**: Neural Networks and Deep Learning (EC460)  
**Institution**: National Institute of Technology Karnataka, Surathkal  
**Supervisor**: Prof. Shyam Lal  
**Team**: Adithya Jayan (181EC102), Anvith M (181EC105)  
**Submission Date**: November 27, 2021

## References

1. Unity Community. "Genetic Algorithm Tutorial in C#." Unity Forum.
2. Bipedal Robot Simulation Research. University of Western Australia, 2002.
3. Wang, S., Chaovalitwongse, W., & Babuska, R. (2012). "Machine learning algorithms in bipedal robot control." IEEE Transactions on Systems, Man, and Cybernetics, Part C, 42(5), 728-743.

## License

This project is licensed under the MIT License.

## Contact

**Adithya Jayan** - [GitHub Profile](https://github.com/Adithya-Jayan)  
**Project Repository** - [https://github.com/Adithya-Jayan/EC460_WalkingBot](https://github.com/Adithya-Jayan/EC460_WalkingBot)
