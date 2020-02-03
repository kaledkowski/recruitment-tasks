using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Basic;
using LightBDD.Framework.Scenarios.Contextual;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using System;

namespace Tests.AcceptanceTests
{
    [FeatureDescription(
        @"As a person
        I want to be able to edit existing note
        So that I could correct errors")]
    public class EditingTests : FeatureFixture
    {
        [Scenario]
        public void TitleEditing()
        {
            var title = Guid.NewGuid().ToString();
            var newTitle = Guid.NewGuid().ToString();
            var content = Guid.NewGuid().ToString();

            Runner.WithContext<EditingContext>().RunScenario(
                _ => _.Given_I_am_on_the_main_page(),
                _ => _.And_there_is_a_sticky_note(title, content),
                _ => _.When_I_click_on_its_title(),
                _ => _.And_I_type_in_a_new_title(newTitle),
                _ => _.And_I_click_on_save_button(),
                _ => _.Then_I_should_see_the_note_with_new_title(newTitle, content));
        }

        [Scenario]
        public void ContentEditing()
        {
            var title = Guid.NewGuid().ToString();
            var content = Guid.NewGuid().ToString();
            var newContent = Guid.NewGuid().ToString();

            Runner.WithContext<EditingContext>().RunScenario(
                _ => _.Given_I_am_on_the_main_page(),
                _ => _.And_there_is_a_sticky_note(title, content),
                _ => _.When_I_click_on_its_content(),
                _ => _.And_I_type_in_a_new_content(newContent),
                _ => _.And_I_click_on_save_button(),
                _ => _.Then_I_should_see_the_note_with_new_content(title, newContent));
        }
    }
}
