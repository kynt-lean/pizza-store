using PizzaStore.Contracts;

namespace PizzaStore.Components;

public class OrderState
{
    public bool ShowingConfigureDialog { get; private set; }

    public PizzaDto? ConfiguringPizza { get; private set; }

    public OrderDto Order { get; private set; } = new();

    public void ShowConfigurePizzaDialog(PizzaSpecialDto special)
    {
        ConfiguringPizza = new PizzaDto()
        {
            Special = special,
            SpecialId = special.Id,
            Size = PizzaDto.DefaultSize
        };
        ShowingConfigureDialog = true;
    }

    public void CancelConfigurePizzaDialog()
    {
        ConfiguringPizza = null;
        ShowingConfigureDialog = false;
    }

    public void ConfirmConfigurePizzaDialog()
    {
        if (ConfiguringPizza is not null)
        {
            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;
        }
        ShowingConfigureDialog = false;
    }

    public void RemoveConfiguredPizza(PizzaDto pizza)
    {
        Order.Pizzas.Remove(pizza);
    }

    public void ResetOrder()
    {
        Order = new OrderDto();
    }
}