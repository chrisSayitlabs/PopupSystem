## Introduction
It is recommended, but not required to use [UPM Git Extensions](https://github.com/mob-sakai/UpmGitExtension) to enhance your ability to use git packages within Unitys Package Manager.

## Installation

Within the Unity Editor, go to Window > Package Manager. Click the "+" button in the top left and select "Add package by git URL..." then copy the URL below into the text box and click "add".

```sh
https://github.com/chrisSayitlabs/PopupSystem.git
```

If you get errors that the "nl.jeffreylanters.tweens" cannot be found, add a scoped registery with the following info to your Packages/manifest.json file:

```sh
    "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "nl.jeffreylanters.tweens"
      ]
    }
  ]
```

Alternatively, within the Editor you can add a scoped registry by going to Edit> Project Settings> Package Manager and add the following:

![Unity_E6mDSAKprT](https://github.com/user-attachments/assets/068bb5fd-226c-4415-b212-6e707d8b5e57)


## Get Started
Check out the SimpleSetup sample project to see how you easily implement the system.

## Suggestions
Annoy Chris with any new features!
