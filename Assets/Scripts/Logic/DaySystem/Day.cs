using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day : MonoBehaviour
{
    [SerializeField] private GameObject _panelEndDay;

    [SerializeField] private TextMeshProUGUI _textOrdersAmount;
    [SerializeField] private TextMeshProUGUI _textEarnMoney;
    [SerializeField] private TextMeshProUGUI _textSpentMoney;
    [SerializeField] private TextMeshProUGUI _textProfitMoney;

    [SerializeField] private DrawStations _drawStations;

    private List<RouteInfo> _routes = new List<RouteInfo>();

    private void Awake()
    {
        InitializeDay();
    }

    private void OnEnable()
    {
        GameTime.Instance.EndDay += EndDay;
    }

    private void OnDisable()
    {
        GameTime.Instance.EndDay -= EndDay;
    }

    public void AddRoute(RouteInfo routeInfo)
    {
        if(!_routes.Contains(routeInfo))
            _routes.Add(routeInfo);
    }

    public void InitializeDay()
    {
        _drawStations.Draw();
        GameTime.Instance.StartNewDay();
    }

    public void NewDay()
    {
        SceneManager.LoadScene(0);
    }

    public void EndDay()
    {
        _panelEndDay.SetActive(true);
        int earned = 0;
        int spent = 0;
        int profit = 0;

        foreach (var route in _routes)
        {
            foreach(var order in route.orders)
            {
                earned += order.payment;
            }
        }

        foreach(var route in _routes)
        {
            RouteStatisticsService statisticsService = new RouteStatisticsService(route);
            spent += (int)statisticsService.GetFuelCost();
        }

        profit = earned - spent;

        _textOrdersAmount.text = $"<color=green>{_routes.Count}</color>";
        _textEarnMoney.text = $"<color=green>{earned}</color>ð";
        _textSpentMoney.text = $"<color=green>{spent}</color>ð";
        _textProfitMoney.text = $"<color=green>{profit}</color>ð";
    }
}
