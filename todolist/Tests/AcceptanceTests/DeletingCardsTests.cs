using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;

namespace Tests.AcceptanceTests
{
    [FeatureDescription(
        @"As a person
        I want to be able to delete a sticky note
        So I would not keep unnecessary reminders")]
    public partial class DeletingCardsTests
    {
        [Scenario]
        public void Scenario()
        {
            Runner.RunScenario(
                Given => I_am_on_the_main_page(),
                And => There_is_a_sticky_note(),
                When => I_click_the_x_icon(),
                Then => I_should_not_see_the_card_anymore());
        }
    }
}
