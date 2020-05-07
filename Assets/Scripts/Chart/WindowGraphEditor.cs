using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraphEditor : MonoBehaviour
{
    [SerializeField] private WindowGraph windowGraph;
    [SerializeField] private Button barChartBtn;
    [SerializeField] private Button lineChartBtn;
    [SerializeField] private Button zoomMinusBtn;
    [SerializeField] private Button zoomPlusBtn;

    private void Start()
    {
        barChartBtn.onClick.AddListener(SetGraphBarClick);
        lineChartBtn.onClick.AddListener(SetGraphLineClick);
        zoomMinusBtn.onClick.AddListener(ZoomMinusClick);
        zoomPlusBtn.onClick.AddListener(ZoomPlusClick);
    }

    private void SetGraphLineClick() => windowGraph.ChangeGraph(0);
    private void SetGraphBarClick() => windowGraph.ChangeGraph(1);
    private void ZoomMinusClick() => windowGraph.ZoomGraph(false);
    private void ZoomPlusClick() => windowGraph.ZoomGraph(true);
}
