using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LiesDatabase database = null;
    public ParticleSystem normalAsteroids = null;
    public GameObject minePrefab = null;
    public Text levelText = null;
    public Text timeText = null;

    public string templateId = "test";
    public int level = -1;

    public StartTextScript startTextScript;

    void Start()
    {
        //startTextScript = GameObject.FindObjectOfType<StartTextScript>();
        EndStage();
    }

    private void Update()
    {
        int time = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        timeText.text = string.Format(
            "{0:00}:{1:00}",
            time / 60,
            time % 60
        );
    }

    private void StartStage(string value)
    {
        templateId = value;
        database.templateId = value;
        database.GetRandomMessages(list => StartCoroutine(ShowMessages(list)));
    }

    private IEnumerator ShowMessages(GetRandomMessages_Result[] list)
    {
        /*
        foreach (var msg in list)
        {
            startTextScript.showMessage(msg.Text);
            yield return new WaitForSeconds(5);
        }
        */

        //foreach(var msg in list)
        //{
        //    Debug.Log(msg.Text);
        //}
        startTextScript.showMessage(list[0].Text);
        yield return new WaitForSeconds(1);
    }

    private void EndStage()
    {
        ++level;
        database.level = level;
        levelText.text = string.Format("Level {0:00}", level + 1);

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
        var em = normalAsteroids.emission;
        em.rateOverTime = 0.0f;
        yield return new WaitForSeconds(5.0f);
        em.rateOverTime = 2.0f;
        yield return new WaitForSeconds(15.0f);
        em.rateOverTime = 3.0f;
        yield return new WaitForSeconds(5.0f);
        em.rateOverTime = 0.0f;
        yield return new WaitForSeconds(5.0f);
        EndStage();
    }

    private IEnumerator template_asteroids_mines()
    {
        StartStage("asteroids_mines");
        var em = normalAsteroids.emission;
        em.rateOverTime = 0.1f;

        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 6; ++j)
            {
                GameObject mine = GameObject.Instantiate(
                    minePrefab,
                    transform.position + Vector3.up * 20 + Vector3.right * Random.Range(-10, 10),
                    Quaternion.identity
                );
                GameObject.Destroy(mine, 20.0f);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(3.0f);
        }
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
