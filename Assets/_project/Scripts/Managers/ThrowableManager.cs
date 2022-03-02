using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Es recomendable tener 2 sets de items, por si decide castear la habilidad 2 veces seguidas
//Los throwables nunca se tienen que destruir, set active false siempre
public class ThrowableManager : MonoBehaviour
{
    [Serializable]
    struct Group
    {
        [Tooltip("Name of the throwable")]
        public string name;
        [Tooltip("Dejar en 0 si es infinito")]
        public float maxTime;
        [Tooltip("Point from where the throwables are launched")]
        public Transform throwPoint;
        [Tooltip("List of all avalible throwables.\nIt is recomended to make several sets")]
        public List<GameObject> throwables;
    }

    [SerializeField] List<Group> groups = new List<Group>();
    private int index;

    private void Awake()
    {
        index = 0;
        groups.ForEach(x => x.throwables.ForEach(i => i.SetActive(false)));
    }

    public void Throw(string groupName)
    {
        Group group;
        try
        {
            group = groups.Find(x => x.name == groupName);
        }
        catch (Exception e)
        {
            Debug.LogError("Group Not Found!: " + groupName + "\n" + e);
            return;
        }

        group.throwables[index].transform.SetPositionAndRotation(group.throwPoint.position, group.throwPoint.rotation);
        group.throwables[index].SetActive(true);
        if (group.maxTime > 0)
            StartCoroutine(Timer(group.throwables[index], group.maxTime));
        index++;

        if (index >= group.throwables.Count)
            index = 0;

        Debug.Log(index);
    }

    IEnumerator Timer(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
