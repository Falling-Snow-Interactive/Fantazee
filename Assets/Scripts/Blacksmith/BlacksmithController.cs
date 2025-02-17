using Fsi.Roguelite;
using ProjectYahtzee.Blacksmith.Cart;
using ProjectYahtzee.Items;

namespace ProjectYahtzee.Blacksmith
{
    public class BlacksmithController : BlacksmithController<Item>
    {
        private BlacksmithCart cart;

        private void Start()
        {
            cart = new BlacksmithCart();
        }
        
        
    }
}