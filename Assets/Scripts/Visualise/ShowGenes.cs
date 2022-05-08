using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGenes : MonoBehaviour
{
    public List<GameObject> genomes;
    public GameObject genePrefab;
    public GameObject genomePrefab;
    public void Show(List<ConnectionGene> genes)
    {
        GameObject genomeObj = Instantiate(genomePrefab, transform);
        foreach (ConnectionGene gene in genes) SpawnGene(gene, genomeObj.transform);
    }
    private void SpawnGene(ConnectionGene gene, Transform parent)
    {
        SetText(Instantiate(genePrefab, parent), gene);
    }
    private void SetText(GameObject gameObject, ConnectionGene gene)
    {
        if (!gene.enabled) gameObject.GetComponent<Image>().color = Color.red;
        else gameObject.GetComponent<Image>().color = Color.grey;

        gameObject.transform.GetChild(0).GetComponent<Text>().text = gene.innovationNumber.ToString();
        gameObject.transform.GetChild(1).GetComponent<Text>().text = $"{gene.from.innovationNumber} -> {gene.to.innovationNumber}";
        gameObject.transform.GetChild(2).GetComponent<Text>().text = ((float)(int)(gene.weight * 1000) / 1000f).ToString();
    }
}
