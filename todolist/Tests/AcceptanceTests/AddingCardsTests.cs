using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;

namespace Tests.AcceptanceTests
{
    [FeatureDescription(
        @"As a person
        I want to be able to add a sticky note
        So I would not forget about important stuff")]
    public partial class AddingCardsTests
    {
        [Scenario]
        public void Scenario()
        {
            Runner.RunScenario(
                Given => I_am_on_the_main_page(),
                When => I_type_in_a_note_title(),
                And => I_type_in_a_note_contents(),
                And => I_click_the_add_card_button(),
                Then => I_should_see_the_card_appended_to_the_list());
        }
    }
}
