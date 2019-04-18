using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject player;
    public Vector2 playerSpawnPosition = new Vector2();
    public Character[] characters;

    public GameObject characterSelectPanel;
    public GameObject skillsPanel;

    public void StartGame(int characterChoice)
    {
        characterSelectPanel.SetActive(false);
        skillsPanel.SetActive(true);

        GameObject spawnedPlayer = Instantiate(player, playerSpawnPosition, Quaternion.identity) as GameObject;
        //SkillCooldown[] cooldownButtons = GetComponentsInChildren<SkillCooldown>();
        Character selectedCharacter = characters[characterChoice];
    }
}
