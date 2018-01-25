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




#Markdown Reference, DEMO CONTENT

--------------------------------------------------------
--------------------------------------------------------
--------------------------------------------------------
--------------------------------------------------------
DON'T DEMO; 
READ INSIDE
> **Note:**

> - Full access to **Google Drive** or **Dropbox** is required to be able to import any document in StackEdit. Permission restrictions can be configured in the settings.
> - Imported documents are downloaded in your browser and are not transmitted to a server.
> - If you experience problems saving your documents on Google Drive, check and optionally disable browser extensions, such as Disconnect.
Once you are happy with your document, you can publish it on different websites directly from StackEdit. As for now, StackEdit can publish on **Blogger**, **Dropbox**, **Gist**, **GitHub**, **Google Drive**, **Tumblr**, **WordPress** and on any SSH server.

> **Note:** If the file has been removed from the website or the blog, the document will no longer be published on that location.
### Mark



**Markdown Extra** has a special syntax for definition lists too:

Term 1
Term 2
:   Definition A
:   Definition B

Term 3

:   Definition C

:   Definition D

	> part of definition D


#### Fenced code blocks

GitHub's fenced code blocks are also supported with **Highlight.js** syntax highlighting:

```
// Foo
var bar = 0;
```

#### SmartyPants

SmartyPants converts ASCII punctuation characters into "smart" typographic punctuation HTML entities. For example:

|                  | ASCII                        | HTML              |
 ----------------- | ---------------------------- | ------------------
| Single backticks | `'Isn't this fun?'`            | 'Isn't this fun?' |
| Quotes           | `"Isn't this fun?"`            | "Isn't this fun?" |
| Dashes           | `-- is en-dash, --- is em-dash` | -- is en-dash, --- is em-dash |

