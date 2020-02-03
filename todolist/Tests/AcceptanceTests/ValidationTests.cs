using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;

namespace Tests.AcceptanceTests
{
    [FeatureDescription(
        @"As a person
        I want to see validation warnings
        So I would not be able to create invalid notes")]
    public partial class ValidationTests
    { 
        [Scenario]
        public void TitleValidation()
        {
            Runner.RunScenario(
                Given => I_am_on_the_main_page(),
                When => I_type_in_a_note_contents("fake content"),
                And => I_click_the_add_card_button(),
                Then => I_should_see_validation_message_for_title());
        }

        [Scenario]
        public void ContentValidation()
        {
            Runner.RunScenario(
                Given => I_am_on_the_main_page(),
                When => I_type_in_a_note_title("fake title"),
                And => I_click_the_add_card_button(),
                Then => I_should_see_validation_message_for_content());
        }
    }
}
