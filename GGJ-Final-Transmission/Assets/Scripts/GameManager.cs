using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LiesDatabase database = null;
    public string templateId = "test";
    public int level = -1;
    
    void Start()
    {
        EndStage();
    }

    private void StartStage(string value)
    {
        templateId = value;
        database.templateId = value;
    }

    private void EndStage()
    {
        ++level;
        database.level = level;

        switch (level % 6)
        {
            default:
            case 0:
                StartCoroutine(template_asteroids_normal());
                break;
            case 1:
                StartCoroutine(template_asteroids_mines());
                break;
            case 2:
                StartCoroutine(template_enemy_normal());
                break;
            case 3:
                StartCoroutine(template_enemy_playerram());
                break;
            case 4:
                StartCoroutine(template_blackholes());
                break;
            case 5:
                StartCoroutine(template_wormholes());
                break;
        }
    }
	
    private IEnumerator template_asteroids_normal()
    {
        StartStage("asteroids_normal");
        yield return new WaitForSeconds(1.0f);
        EndStage();
    }

    private IEnumerator template_asteroids_mines()
    {
        StartStage("asteroids_mines");
        yield return new WaitForSeconds(1.0f);
        EndStage();
    }

    private IEnumerator template_enemy_normal()
    {
        StartStage("enemy_normal");
        yield return new WaitForSeconds(1.0f);
        EndStage();
    }

    private IEnumerator template_enemy_playerram()
    {
        StartStage("enemy_playerram");
        yield return new WaitForSeconds(1.0f);
        EndStage();
    }

    private IEnumerator template_blackholes()
    {
        StartStage("blackholes");
        yield return new WaitForSeconds(1.0f);
        EndStage();
    }

    private IEnumerator template_wormholes()
    {
        StartStage("wormholes");
        yield return new WaitForSeconds(1.0f);
        EndStage();
    }

}
