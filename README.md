# Augmented Reality Assignment

## Overview

This project is an Augmented Reality (AR) application developed using Unity, AR Foundation, and Firebase Hosting. The application allows users to place a transparent video overlay onto detected vertical surfaces in the real world and dynamically switch between remotely hosted videos.

The project demonstrates the integration of:

- Unity AR Foundation
- ARCore (Android)
- Unity Addressables
- Firebase Hosting
- Remote Video Asset Loading
- Runtime Video Switching

---

## Features

### Wall Detection and Placement
- Detects vertical surfaces using AR Foundation.
- Allows users to place a video quad on a detected wall using touch interaction.
- Automatically aligns the video content with the detected wall surface.

### Transparent Video Playback
- Supports transparent video rendering.
- Videos are displayed as overlays on real-world walls.

### Remote Content Delivery
- Video assets are hosted remotely.
- Unity Addressables are used to fetch assets dynamically at runtime.
- Firebase Hosting is used to serve Addressables catalogs and asset bundles.

### Video Switching
- Users can switch between multiple videos using UI buttons.
- Videos are loaded remotely and played without requiring application updates.

---

## Technologies Used

- Unity
- AR Foundation
- ARCore
- Unity Addressables
- Firebase Hosting
- C#
- Android

---

## Project Structure

```text
Assets
│
├── Scripts
│   ├── TapToPlace.cs
│   └── VideoLoader.cs
│
├── Prefabs
│   └── VideoQuad
│
├── Addressables

```

---

## Workflow

### 1. Wall Detection
The application scans the environment and detects wall surfaces using AR Foundation plane detection.

### 2. Video Placement
When the user taps a detected wall:

- A VideoQuad prefab is instantiated.
- The video surface is aligned with the detected wall.
- Video playback controls become visible.

### 3. Remote Asset Loading
At startup:

- The application loads the remote Addressables catalog from Firebase Hosting.
- Video assets are downloaded dynamically.

### 4. Video Playback
The selected video is assigned to a Unity VideoPlayer and rendered onto the wall surface.

### 5. Video Switching
Users can switch between available videos through UI controls.

---

## Firebase Hosting

The Addressables catalog and asset bundles are hosted on Firebase Hosting.

---

## Installation

### Prerequisites

- Unity 2022 or later
- AR Foundation
- ARCore XR Plugin
- Android Device with ARCore support

### Steps

1. Clone the repository

```bash
git clone <repository-url>
```

2. Open the project in Unity.

3. Install required packages:
   - AR Foundation
   - ARCore XR Plugin
   - Addressables

4. Build Addressables.

5. Deploy Addressables content to Firebase Hosting.

6. Build and run on an ARCore-supported Android device.

---

## Learning Outcomes

This project demonstrates:

- AR wall detection and placement
- Remote asset management using Addressables
- Firebase Hosting integration
- Runtime video playback and switching
- Mobile AR application development using Unity

---
