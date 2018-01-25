# Shutdown

# Shutdown!

Shutdown is an extremely versatile tool used by customers mainly residing in Europe and North America. We provide an exact solution to the second most important action feasible on personal computers - shutting them down.
This is achieved by employing a sophisticated algorithm used by companies as Microsoft **Microsoft**[^Microsoft]

----------

## Table of contents
[TOC]

## Ambition
-------------

Our goal is to provide users with the most comfortable solution for a timer-based shutdown inquiry. This is done by implementing an **extended** version of this algorithm paired with the **Material Design Guideline**[^Material].

## Design
-------------------
Although the design is based off Material, slight alterations have been made to enhance the user experience when using the program for the typical short period of time. This ensures a smooth transition from opening the program to closing it, to shutting down the machine. Imagine a single button executing a planned shutdown, 
yet automagically shutting down the program as well, reacting the way **you, the user** wishes to.


----------

## Diagrams

### Sequence Diagram

Here you can witness the seamless transition from human language to the executed command:

```sequence
User->Shutdown: Shutdown in 90 minutes
Shutdown->PC: Shutdown in 90 minutes
Note right of PC: PC executes command
PC->Shutdown: Successfully planned shutdown
Shutdown->User: Successfully planned shutdown
```
As you can see the transition is happening **highly symmetrical**, ensuring information is transported in the most **efficient manner**.

### Flow Diagram

Naturally the sequence diagram is rather coarse-grained, representing basic functionality, whereas the flow-diagram depicts a **real world use-case**:

```flow
st=>start: Start
e=>end
op=>operation: Shutdown
cond=>condition: Yes or No?

st->op->cond
cond(yes)->e
cond(no)->op
```

> **Note:** You can find more information:

> - about **Sequence diagrams** syntax [here][1],
> - about **Flow charts** syntax [here][2].


  [^microsoft]: [Microsoft] https://msdn.microsoft.com/en-us/library/windows/desktop/aa376883(v=vs.85).aspx (Reference for C++)
  
  [^material]: [Material Design Guidelines] https://material.io/guidelines/#

  [1]: http://bramp.github.io/js-sequence-diagrams/
  [2]: http://adrai.github.io/flowchart.js/

Usage
-------------
As soon as the minimal lovable version is released information on how to use it will appear *right here*! We're working hard on keeping this paragraph as short as possible in the future and appreciate your patience!



