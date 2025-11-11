using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magisterka2
{

    public class Secondary_averave : SecondaryBase
    {


        //public Rank rank { get; set; }
        public double allInPings { get; set; }
        public double assistMePings { get; set; }
        public double assists { get; set; }
        public double baronKills { get; set; }
        public double bountyLevel { get; set; }
        public double commandPings { get; set; }
        public double consumablesPurchased { get; set; }
        public double damageDealtToBuildings { get; set; }
        public double damageDealtToObjectives { get; set; }
        public double damageDealtToTurrets { get; set; }
        public double damageSelfMitigated { get; set; }
        public double deaths { get; set; }
        public double detectorWardsPlaced { get; set; }
        public double doubleKills { get; set; }
        public double dragonKills { get; set; }
        public double eligibleForProgression { get; set; }
        public double enemyMissingPings { get; set; }
        public double enemyVisionPings { get; set; }
        public double firstBloodAssist { get; set; }
        public double firstBloodKill { get; set; }
        public double firstTowerAssist { get; set; }
        public double firstTowerKill { get; set; }
        public double gameEndedInEarlySurrender { get; set; }
        public double gameEndedInSurrender { get; set; }
        public double getBackPings { get; set; }
        public double goldEarned { get; set; }
        public double goldSpent { get; set; }
        public double holdPings { get; set; }
        public string? individualPosition { get; set; }
        public double inhibitorKills { get; set; }
        public double inhibitorTakedowns { get; set; }
        public double inhibitorsLost { get; set; }
        public double itemsPurchased { get; set; }
        public double killingSprees { get; set; }
        public double kills { get; set; }
        public string? lane { get; set; }
        public double largestCriticalStrike { get; set; }
        public double largestKillingSpree { get; set; }
        public double largestMultiKill { get; set; }
        public double longestTimeSpentLiving { get; set; }
        public double magicDamageDealt { get; set; }
        public double magicDamageDealtToChampions { get; set; }
        public double magicDamageTaken { get; set; }
        public double needVisionPings { get; set; }
        public double neutralMinionsKilled { get; set; }
        public double nexusKills { get; set; }
        public double nexusLost { get; set; }
        public double nexusTakedowns { get; set; }
        public double objectivesStolen { get; set; }
        public double objectivesStolenAssists { get; set; }
        public double onMyWayPings { get; set; }
        public double pentaKills { get; set; }
        public double physicalDamageDealt { get; set; }
        public double physicalDamageDealtToChampions { get; set; }
        public double physicalDamageTaken { get; set; }
        public double pushPings { get; set; }
        public string? puuid { get; set; }
        public double quadraKills { get; set; }
        public double retreatPings { get; set; }
        public string? riotIdGameName { get; set; }
        public string? riotIdTagline { get; set; }
        public string? role { get; set; }
        public double spell1Casts { get; set; }
        public double spell2Casts { get; set; }
        public double spell3Casts { get; set; }
        public double spell4Casts { get; set; }
        public double summoner1Casts { get; set; }
        public double summoner1Id { get; set; }
        public double summoner2Casts { get; set; }
        public double summoner2Id { get; set; }
        public string? summonerId { get; set; }
        public double summonerLevel { get; set; }
        public string? summonerName { get; set; }
        public double teamEarlySurrendered { get; set; }
        public double timeCCingOthers { get; set; }
        public double timePlayed { get; set; }
        public double totalAllyJungleMinionsKilled { get; set; }
        public double totalDamageDealt { get; set; }
        public double totalDamageDealtToChampions { get; set; }
        public double totalDamageShieldedOnTeammates { get; set; }
        public double totalDamageTaken { get; set; }
        public double totalEnemyJungleMinionsKilled { get; set; }
        public double totalHeal { get; set; }
        public double totalHealsOnTeammates { get; set; }
        public double totalMinionsKilled { get; set; }
        public double totalTimeCCDealt { get; set; }
        public double totalTimeSpentDead { get; set; }
        public double totalUnitsHealed { get; set; }
        public double tripleKills { get; set; }
        public double trueDamageDealt { get; set; }
        public double trueDamageDealtToChampions { get; set; }
        public double trueDamageTaken { get; set; }
        public double turretKills { get; set; }
        public double turretTakedowns { get; set; }
        public double turretsLost { get; set; }
        public double visionClearedPings { get; set; }
        public double visionScore { get; set; }
        public double visionWardsBoughtInGame { get; set; }
        public double wardsKilled { get; set; }
        public double wardsPlaced { get; set; }
        public double win { get; set; }

        //Challanges

        public double HealFromMapSources { get; set; }
        public double InfernalScalePickup { get; set; }
        public double abilityUses { get; set; }
        public double alliedJungleMonsterKills { get; set; }
        public double baronTakedowns { get; set; }
        public double blastConeOppositeOpponentCount { get; set; }
        public double bountyGold { get; set; }
        public double buffsStolen { get; set; }
        public double controlWardTimeCoverageInRiverOrEnemyHalf { get; set; }
        public double controlWardsPlaced { get; set; }
        public double damagePerMinute { get; set; }
        public double damageTakenOnTeamPercentage { get; set; }
        public double deathsByEnemyChamps { get; set; }
        public double dodgeSkillShotsSmallWindow { get; set; }
        public double dragonTakedowns { get; set; }
        public double effectiveHealAndShielding { get; set; }
        public double elderDragonKillsWithOpposingSoul { get; set; }
        public double elderDragonMultikills { get; set; }
        public double enemyChampionImmobilizations { get; set; }
        public double enemyJungleMonsterKills { get; set; }
        public double epicMonsterKillsNearEnemyJungler { get; set; }
        public double epicMonsterKillsWithin30SecondsOfSpawn { get; set; }
        public double epicMonsterSteals { get; set; }
        public double epicMonsterStolenWithoutSmite { get; set; }
        public double fullTeamTakedown { get; set; }
        public double gameLength { get; set; }
        public double getTakedownsInAllLanesEarlyJungleAsLaner { get; set; }
        public double goldPerMinute { get; set; }
        public double immobilizeAndKillWithAlly { get; set; }
        public double initialBuffCount { get; set; }
        public double initialCrabCount { get; set; }
        public double jungleCsBefore10Minutes { get; set; }
        public double junglerTakedownsNearDamagedEpicMonster { get; set; }
        public double kTurretsDestroyedBeforePlatesFall { get; set; }
        public double kda { get; set; }
        public double killAfterHiddenWithAlly { get; set; }
        public double killParticipation { get; set; }
        public double killedChampTookFullTeamDamageSurvived { get; set; }
        public double killsNearEnemyTurret { get; set; }
        public double killsOnOtherLanesEarlyJungleAsLaner { get; set; }
        public double killsUnderOwnTurret { get; set; }
        public double killsWithHelpFromEpicMonster { get; set; }
        public double knockEnemyIntoTeamAndKill { get; set; }
        public double landSkillShotsEarlyGame { get; set; }
        public double laneMinionsFirst10Minutes { get; set; }
        public double laningPhaseGoldExpAdvantage { get; set; }
        public double legendaryCount { get; set; }
        public double maxCsAdvantageOnLaneOpponent { get; set; }
        public double maxKillDeficit { get; set; }
        public double maxLevelLeadLaneOpponent { get; set; }
        public double moreEnemyJungleThanOpponent { get; set; }
        public double multiKillOneSpell { get; set; }
        public double multiTurretRiftHeraldCount { get; set; }
        public double multikills { get; set; }
        public double multikillsAfterAggressiveFlash { get; set; }
        public double outnumberedKills { get; set; }
        public double outnumberedNexusKill { get; set; }
        public double perfectDragonSoulsTaken { get; set; }
        public double perfectGame { get; set; }
        public double pickKillWithAlly { get; set; }
        public double quickCleanse { get; set; }
        public double quickFirstTurret { get; set; }
        public double quickSoloKills { get; set; }
        public double riftHeraldTakedowns { get; set; }
        public double saveAllyFromDeath { get; set; }
        public double scuttleCrabKills { get; set; }
        public double skillshotsDodged { get; set; }
        public double skillshotsHit { get; set; }
        public double soloBaronKills { get; set; }
        public double soloKills { get; set; }
        public double stealthWardsPlaced { get; set; }
        public double survivedSingleDigitHpCount { get; set; }
        public double survivedThreeImmobilizesInFight { get; set; }
        public double takedownOnFirstTurret { get; set; }
        public double takedowns { get; set; }
        public double takedownsAfterGainingLevelAdvantage { get; set; }
        public double takedownsBeforeJungleMinionSpawn { get; set; }
        public double takedownsFirstXMinutes { get; set; }
        public double takedownsInAlcove { get; set; }
        public double takedownsInEnemyFountain { get; set; }
        public double teamBaronKills { get; set; }
        public double teamDamagePercentage { get; set; }
        public double teamElderDragonKills { get; set; }
        public double teamRiftHeraldKills { get; set; }
        public double tookLargeDamageSurvived { get; set; }
        public double turretPlatesTaken { get; set; }
        public double turretsTakenWithRiftHerald { get; set; }
        public double twentyMinionsIn3SecondsCount { get; set; }
        public double twoWardsOneSweeperCount { get; set; }
        public double visionScoreAdvantageLaneOpponent { get; set; }
        public double visionScorePerMinute { get; set; }
        public double voidMonsterKill { get; set; }
        public double wardTakedowns { get; set; }
        public double wardTakedownsBefore20M { get; set; }
        public double wardsGuarded { get; set; }
        public double earliestDragonTakedown { get; set; }
        public double junglerKillsEarlyJungle { get; set; }
        public double killsOnLanersEarlyJungleAsJungler { get; set; }
        public double baronBuffGoldAdvantageOverThreshold { get; set; }
        public double earliestBaron { get; set; }
        public double firstTurretKilledTime { get; set; }
        public double highestChampionDamage { get; set; }
        public double soloTurretsLategame { get; set; }
        public double fasterSupportQuestCompletion { get; set; }
        public double highestCrowdControlScore { get; set; }

        //teams

        public double atakhan { get; set; }
        public double atakhan_first { get; set; }
        public double baron { get; set; }
        public double baron_first { get; set; }
        public double champion { get; set; }
        public double champion_first { get; set; }
        public double dragon { get; set; }
        public double dragon_first { get; set; }
        public double horde { get; set; }
        public double horde_first { get; set; }
        public double inhibitor { get; set; }
        public double inhibitor_first { get; set; }
        public double riftHerald { get; set; }
        public double riftHerald_first { get; set; }
        public double tower { get; set; }
        public double tower_first { get; set; }

        public Secondary_averave() { }
        /*public Secondary(Rank rank, Primary primary)
        {
            this.rank = rank;
            #region Participant
            allInPings = primary.allInPings;
            assistMePings = primary.assistMePings;
            assists = primary.assists;
            baronKills = primary.baronKills;

            bountyLevel = primary.bountyLevel;

            commandPings = primary.commandPings;
            consumablesPurchased = primary.consumablesPurchased;
            damageDealtToBuildings = primary.damageDealtToBuildings;
            damageDealtToObjectives = primary.damageDealtToObjectives;
            damageDealtToTurrets = primary.damageDealtToTurrets;
            damageSelfMitigated = primary.damageSelfMitigated;

            deaths = primary.deaths;
            detectorWardsPlaced = primary.detectorWardsPlaced;
            doubleKills = primary.doubleKills;
            dragonKills = primary.dragonKills;
            eligibleForProgression = primary.eligibleForProgression;
            enemyMissingPings = primary.enemyMissingPings;
            enemyVisionPings = primary.enemyVisionPings;
            firstBloodAssist = primary.firstBloodAssist;
            firstBloodKill = primary.firstBloodKill;
            firstTowerAssist = primary.firstTowerAssist;
            firstTowerKill = primary.firstTowerKill;
            gameEndedInEarlySurrender = primary.gameEndedInEarlySurrender;
            gameEndedInSurrender = primary.gameEndedInSurrender;
            getBackPings = primary.getBackPings;
            goldEarned = primary.goldEarned;
            goldSpent = primary.goldSpent;
            holdPings = primary.holdPings;
            individualPosition = primary.individualPosition;
            inhibitorKills = primary.inhibitorKills;
            inhibitorTakedowns = primary.inhibitorTakedowns;
            inhibitorsLost = primary.inhibitorsLost;

            itemsPurchased = primary.itemsPurchased;
            killingSprees = primary.killingSprees;
            kills = primary.kills;
            lane = primary.lane;
            largestCriticalStrike = primary.largestCriticalStrike;
            largestKillingSpree = primary.largestKillingSpree;
            largestMultiKill = primary.largestMultiKill;
            longestTimeSpentLiving = primary.longestTimeSpentLiving;
            magicDamageDealt = primary.magicDamageDealt;
            magicDamageDealtToChampions = primary.magicDamageDealtToChampions;
            magicDamageTaken = primary.magicDamageTaken;
            needVisionPings = primary.needVisionPings;
            neutralMinionsKilled = primary.neutralMinionsKilled;
            nexusKills = primary.nexusKills;
            nexusLost = primary.nexusLost;
            nexusTakedowns = primary.nexusTakedowns;
            objectivesStolen = primary.objectivesStolen;
            objectivesStolenAssists = primary.objectivesStolenAssists;
            onMyWayPings = primary.onMyWayPings;

            pentaKills = primary.pentaKills;
            physicalDamageDealt = primary.physicalDamageDealt;
            physicalDamageDealtToChampions = primary.physicalDamageDealtToChampions;
            physicalDamageTaken = primary.physicalDamageTaken;

            pushPings = primary.pushPings;
            puuid = primary.puuid;
            quadraKills = primary.quadraKills;
            retreatPings = primary.retreatPings;
            riotIdGameName = primary.riotIdGameName;
            riotIdTagline = primary.riotIdTagline;
            role = primary.role;

            spell1Casts = primary.spell1Casts;
            spell2Casts = primary.spell2Casts;
            spell3Casts = primary.spell3Casts;
            spell4Casts = primary.spell4Casts;

            summoner1Casts = primary.summoner1Casts;
            summoner1Id = primary.summoner1Id;
            summoner2Casts = primary.summoner2Casts;
            summoner2Id = primary.summoner2Id;
            summonerId = primary.summonerId;
            summonerLevel = primary.summonerLevel;
            summonerName = primary.summonerName;
            teamEarlySurrendered = primary.teamEarlySurrendered;

            timeCCingOthers = primary.timeCCingOthers;
            timePlayed = primary.timePlayed;
            totalAllyJungleMinionsKilled = primary.totalAllyJungleMinionsKilled;
            totalDamageDealt = primary.totalDamageDealt;
            totalDamageDealtToChampions = primary.totalDamageDealtToChampions;
            totalDamageShieldedOnTeammates = primary.totalDamageShieldedOnTeammates;
            totalDamageTaken = primary.totalDamageTaken;
            totalEnemyJungleMinionsKilled = primary.totalEnemyJungleMinionsKilled;
            totalHeal = primary.totalHeal;
            totalHealsOnTeammates = primary.totalHealsOnTeammates;
            totalMinionsKilled = primary.totalMinionsKilled;
            totalTimeCCDealt = primary.totalTimeCCDealt;
            totalTimeSpentDead = primary.totalTimeSpentDead;
            totalUnitsHealed = primary.totalUnitsHealed;
            tripleKills = primary.tripleKills;
            trueDamageDealt = primary.trueDamageDealt;
            trueDamageDealtToChampions = primary.trueDamageDealtToChampions;
            trueDamageTaken = primary.trueDamageTaken;
            turretKills = primary.turretKills;
            turretTakedowns = primary.turretTakedowns;
            turretsLost = primary.turretsLost;

            visionClearedPings = primary.visionClearedPings;
            visionScore = primary.visionScore;
            visionWardsBoughtInGame = primary.visionWardsBoughtInGame;
            wardsKilled = primary.wardsKilled;
            wardsPlaced = primary.wardsPlaced;
            win = primary.win;
            #endregion

            #region Challenges


            HealFromMapSources = primary.HealFromMapSources;
            InfernalScalePickup = primary.InfernalScalePickup;
            abilityUses = primary.abilityUses;

            alliedJungleMonsterKills = primary.alliedJungleMonsterKills;
            baronTakedowns = primary.baronTakedowns;
            blastConeOppositeOpponentCount = primary.blastConeOppositeOpponentCount;
            bountyGold = primary.bountyGold;
            buffsStolen = primary.buffsStolen;
            controlWardTimeCoverageInRiverOrEnemyHalf = primary.controlWardTimeCoverageInRiverOrEnemyHalf;
            controlWardsPlaced = primary.controlWardsPlaced;
            damagePerMinute = primary.damagePerMinute;
            damageTakenOnTeamPercentage = primary.damageTakenOnTeamPercentage;
            deathsByEnemyChamps = primary.deathsByEnemyChamps;
            dodgeSkillShotsSmallWindow = primary.dodgeSkillShotsSmallWindow;
            dragonTakedowns = primary.dragonTakedowns;
            effectiveHealAndShielding = primary.effectiveHealAndShielding;
            elderDragonKillsWithOpposingSoul = primary.elderDragonKillsWithOpposingSoul;
            elderDragonMultikills = primary.elderDragonMultikills;
            enemyChampionImmobilizations = primary.enemyChampionImmobilizations;
            enemyJungleMonsterKills = primary.enemyJungleMonsterKills;
            epicMonsterKillsNearEnemyJungler = primary.epicMonsterKillsNearEnemyJungler;
            epicMonsterKillsWithin30SecondsOfSpawn = primary.epicMonsterKillsWithin30SecondsOfSpawn;
            epicMonsterSteals = primary.epicMonsterSteals;
            epicMonsterStolenWithoutSmite = primary.epicMonsterStolenWithoutSmite;
            fullTeamTakedown = primary.fullTeamTakedown;
            gameLength = primary.gameLength;
            getTakedownsInAllLanesEarlyJungleAsLaner = primary.getTakedownsInAllLanesEarlyJungleAsLaner;
            goldPerMinute = primary.goldPerMinute;
            immobilizeAndKillWithAlly = primary.immobilizeAndKillWithAlly;
            initialBuffCount = primary.initialBuffCount;
            initialCrabCount = primary.initialCrabCount;
            jungleCsBefore10Minutes = primary.jungleCsBefore10Minutes;
            junglerTakedownsNearDamagedEpicMonster = primary.junglerTakedownsNearDamagedEpicMonster;
            kTurretsDestroyedBeforePlatesFall = primary.kTurretsDestroyedBeforePlatesFall;
            kda = primary.kda;
            killAfterHiddenWithAlly = primary.killAfterHiddenWithAlly;
            killParticipation = primary.killParticipation;
            killedChampTookFullTeamDamageSurvived = primary.killedChampTookFullTeamDamageSurvived;
            killingSprees = primary.killingSprees;
            killsNearEnemyTurret = primary.killsNearEnemyTurret;
            killsOnOtherLanesEarlyJungleAsLaner = primary.killsOnOtherLanesEarlyJungleAsLaner;
            killsUnderOwnTurret = primary.killsUnderOwnTurret;
            killsWithHelpFromEpicMonster = primary.killsWithHelpFromEpicMonster;
            knockEnemyIntoTeamAndKill = primary.knockEnemyIntoTeamAndKill;
            landSkillShotsEarlyGame = primary.landSkillShotsEarlyGame;
            laneMinionsFirst10Minutes = primary.laneMinionsFirst10Minutes;
            laningPhaseGoldExpAdvantage = primary.laningPhaseGoldExpAdvantage;
            legendaryCount = primary.legendaryCount;
            maxCsAdvantageOnLaneOpponent = primary.maxCsAdvantageOnLaneOpponent;
            maxKillDeficit = primary.maxKillDeficit;
            maxLevelLeadLaneOpponent = primary.maxLevelLeadLaneOpponent;
            moreEnemyJungleThanOpponent = primary.moreEnemyJungleThanOpponent;
            multiKillOneSpell = primary.multiKillOneSpell;
            multiTurretRiftHeraldCount = primary.multiTurretRiftHeraldCount;
            multikills = primary.multikills;
            multikillsAfterAggressiveFlash = primary.multikillsAfterAggressiveFlash;
            outnumberedKills = primary.outnumberedKills;
            outnumberedNexusKill = primary.outnumberedNexusKill;
            perfectDragonSoulsTaken = primary.perfectDragonSoulsTaken;
            perfectGame = primary.perfectGame;
            pickKillWithAlly = primary.pickKillWithAlly;
            quickCleanse = primary.quickCleanse;
            quickFirstTurret = primary.quickFirstTurret;
            quickSoloKills = primary.quickSoloKills;
            riftHeraldTakedowns = primary.riftHeraldTakedowns;
            saveAllyFromDeath = primary.saveAllyFromDeath;
            scuttleCrabKills = primary.scuttleCrabKills;
            skillshotsDodged = primary.skillshotsDodged;
            skillshotsHit = primary.skillshotsHit;

            soloBaronKills = primary.soloBaronKills;
            soloKills = primary.soloKills;
            stealthWardsPlaced = primary.stealthWardsPlaced;
            survivedSingleDigitHpCount = primary.survivedSingleDigitHpCount;
            survivedThreeImmobilizesInFight = primary.survivedThreeImmobilizesInFight;
            takedownOnFirstTurret = primary.takedownOnFirstTurret;
            takedowns = primary.takedowns;
            takedownsAfterGainingLevelAdvantage = primary.takedownsAfterGainingLevelAdvantage;
            takedownsBeforeJungleMinionSpawn = primary.takedownsBeforeJungleMinionSpawn;
            takedownsFirstXMinutes = primary.takedownsFirstXMinutes;
            takedownsInAlcove = primary.takedownsInAlcove;
            takedownsInEnemyFountain = primary.takedownsInEnemyFountain;
            teamBaronKills = primary.teamBaronKills;
            teamDamagePercentage = primary.teamDamagePercentage;
            teamElderDragonKills = primary.teamElderDragonKills;
            teamRiftHeraldKills = primary.teamRiftHeraldKills;
            tookLargeDamageSurvived = primary.tookLargeDamageSurvived;
            turretPlatesTaken = primary.turretPlatesTaken;
            turretTakedowns = primary.turretTakedowns;
            turretsTakenWithRiftHerald = primary.turretsTakenWithRiftHerald;
            twentyMinionsIn3SecondsCount = primary.twentyMinionsIn3SecondsCount;
            twoWardsOneSweeperCount = primary.twoWardsOneSweeperCount;
            visionScoreAdvantageLaneOpponent = primary.visionScoreAdvantageLaneOpponent;
            visionScorePerMinute = primary.visionScorePerMinute;
            voidMonsterKill = primary.voidMonsterKill;
            wardTakedowns = primary.wardTakedowns;
            wardTakedownsBefore20M = primary.wardTakedownsBefore20M;
            wardsGuarded = primary.wardsGuarded;
            earliestDragonTakedown = primary.earliestDragonTakedown;
            junglerKillsEarlyJungle = primary.junglerKillsEarlyJungle;
            killsOnLanersEarlyJungleAsJungler = primary.killsOnLanersEarlyJungleAsJungler;
            baronBuffGoldAdvantageOverThreshold = primary.baronBuffGoldAdvantageOverThreshold;
            earliestBaron = primary.earliestBaron;
            firstTurretKilledTime = primary.firstTurretKilledTime;
            highestChampionDamage = primary.highestChampionDamage;
            soloTurretsLategame = primary.soloTurretsLategame;
            fasterSupportQuestCompletion = primary.fasterSupportQuestCompletion;
            highestCrowdControlScore = primary.highestCrowdControlScore;

            #endregion


            atakhan = primary.atakhan;
            atakhan_first = primary.atakhan_first;

            baron = primary.baron;
            baron_first = primary.baron_first;

            champion = primary.champion;
            champion_first = primary.champion_first;

            dragon = primary.dragon;
            dragon_first = primary.dragon_first;

            horde = primary.horde;
            horde_first = primary.horde_first;

            inhibitor = primary.inhibitor;
            inhibitor_first = primary.inhibitor_first;

            riftHerald = primary.riftHerald;
            riftHerald_first = primary.riftHerald_first;

            tower = primary.tower;
            tower_first = primary.tower_first;

        }
    }*/
    }
    
}
