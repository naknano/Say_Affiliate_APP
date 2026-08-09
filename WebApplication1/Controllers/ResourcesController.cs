using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class ResourcesController : Controller
{
    public IActionResult Index()           => View();
    public IActionResult LearnForexBasics() => View();
    public IActionResult ForexBrokers()    => View();
    public IActionResult ForexTools()      => View();
    public IActionResult ForexNews()       => View();
    public IActionResult Strategies()      => View();
    public IActionResult ForexCashback()   => View();

    /* ── Learn Forex Basics articles ── */
    public IActionResult LearnRebates()           => View("LearnForexBasics/WhatAreRebates");
    public IActionResult LearnForexGuide()        => View("LearnForexBasics/ForexBasicsGuide");
    public IActionResult LearnTradingPlan()       => View("LearnForexBasics/TradingPlan");
    public IActionResult LearnTradeOnBudget()     => View("LearnForexBasics/TradeOnBudget");
    public IActionResult LearnTradingPsychology() => View("LearnForexBasics/TradingPsychology");
    public IActionResult LearnCommonMistakes()    => View("LearnForexBasics/CommonMistakes");

    /* ── Forex Tool articles ── */
    public IActionResult PositionSizeCalculator()    => View("ForexTools/PositionSizeCalculator");
    public IActionResult PositionSizeTradeBetter()   => View("ForexTools/PositionSizeTradeBetter");
    public IActionResult TradingPlatformsAnalysis()  => View("ForexTools/TradingPlatformsAnalysis");
    public IActionResult WhyUseVPS()                 => View("ForexTools/WhyUseVPS");

    /* ── Strategy articles ── */
    public IActionResult StrategyCashback500()    => View("Strategies/Cashback500");
    public IActionResult StrategyGoldRebounds()   => View("Strategies/GoldRebounds");
    public IActionResult StrategyEurUsdRebounds() => View("Strategies/EurUsdRebounds");
    public IActionResult StrategyTechCashback()   => View("Strategies/TechCashback");
    public IActionResult StrategyDayTrading()     => View("Strategies/DayTrading");
    public IActionResult StrategySwingTrading()   => View("Strategies/SwingTrading");
    public IActionResult StrategyTechAnalysis()   => View("Strategies/TechAnalysis");
    public IActionResult StrategyForexSignals()   => View("Strategies/ForexSignals");
    public IActionResult StrategyAdvanced()       => View("Strategies/Advanced");
    public IActionResult StrategyScalping()       => View("Strategies/Scalping");
    public IActionResult StrategyVolatile()       => View("Strategies/Volatile");
    public IActionResult StrategyAlgo()           => View("Strategies/Algo");

    /* ── Forex News articles ── */
    public IActionResult ForexNewsFreePlatform()    => View("ForexNews/FreePlatform");
    public IActionResult ForexNewsChangingGame()    => View("ForexNews/ChangingGame");
    public IActionResult ForexNewsUniqueTools()     => View("ForexNews/UniqueTools");
    public IActionResult ForexNewsRevolutionising() => View("ForexNews/Revolutionising");
    public IActionResult ForexNewsRedefining()      => View("ForexNews/Redefining");

    /* ── Broker review articles ── */
    public IActionResult PuPrimeReview()    => View("ForexBrokers/PuPrimeReview");
    public IActionResult IcMarketsReview()  => View("ForexBrokers/IcMarketsReview");
    public IActionResult FxProReview()           => View("ForexBrokers/FxProReview");
    public IActionResult BestCashbackBrokers()  => View("ForexBrokers/BestCashbackBrokers");
    public IActionResult HowToChooseBroker()   => View("ForexBrokers/HowToChooseBroker");
}
