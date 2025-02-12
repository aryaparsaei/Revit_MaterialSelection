using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn
{
    public class MaterialSelectionScenario
    {
        public double EnvironmentalFactor { get; set; }
        public double EconomicalFactor { get; set; }
        public double SocialFactor { get; set; }

        public MaterialSelectionScenario()
        {
            
        }
        public MaterialSelectionScenario(ScenarioMode scenarioMode)
        {

            switch (scenarioMode)
            {
                case ScenarioMode.EqualWeights:
                    EnvironmentalFactor = 0.33;
                    EconomicalFactor = 0.33;
                    SocialFactor = 0.33;
                    break;
                
                case ScenarioMode.EnvironmentImportant:
                    EnvironmentalFactor = 0.5;
                    EconomicalFactor = .25;
                    SocialFactor = .25;
                    break;

                case ScenarioMode.CostImportant:
                    EnvironmentalFactor = 0.25;
                    EconomicalFactor = 0.5;
                    SocialFactor = 0.25;
                    break;
                case ScenarioMode.SurveyResults:
                    EnvironmentalFactor = 0.43;
                    EconomicalFactor = 0.39;
                    SocialFactor = 0.18;
                    break;
            }

        }

    }

    public enum ScenarioMode
    {
        EqualWeights = 1,
        EnvironmentImportant = 2,
        CostImportant = 3,
        SurveyResults = 4,
        UserWeightsAHP = 5
    }
}
