using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LevelBasis
{
    string levelName { get; set; }
    int totalTasksCount { get; set; }
    int completedTasksCount { get; set; }

    int progressionPhase { get; set; }
    bool hasPlayedBefore { get; set; }

    List<GameObject> allPhaseObjects { get; set; }

    //task controller object

    //unlockables??

    void initLevel();

    void taskCompleted();

    void showcaseLevelProgression();

    void saveProgress();
}
