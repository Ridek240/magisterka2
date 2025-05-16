using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magisterka2
{
    public class Primary
    {
        public string? matchId { get; set; }

        public int PlayerScore0 { get; set; }
        public int PlayerScore1 { get; set; }
        public int PlayerScore10 { get; set; }
        public int PlayerScore11 { get; set; }
        public int PlayerScore2 { get; set; }
        public int PlayerScore3 { get; set; }
        public int PlayerScore4 { get; set; }
        public int PlayerScore5 { get; set; }
        public int PlayerScore6 { get; set; }
        public int PlayerScore7 { get; set; }
        public int PlayerScore8 { get; set; }
        public int PlayerScore9 { get; set; }
        public int allInPings { get; set; }
        public int assistMePings { get; set; }
        public int assists { get; set; }
        public int baronKills { get; set; }
        public int basicPings { get; set; }
        public int bountyLevel { get; set; }
        public int champExperience { get; set; }
        public int champLevel { get; set; }
        public int championId { get; set; }
        public string? championName { get; set; }
        public int championTransform { get; set; }
        public int commandPings { get; set; }
        public int consumablesPurchased { get; set; }
        public int damageDealtToBuildings { get; set; }
        public int damageDealtToObjectives { get; set; }
        public int damageDealtToTurrets { get; set; }
        public int damageSelfMitigated { get; set; }
        public int dangerPings { get; set; }
        public int deaths { get; set; }
        public int detectorWardsPlaced { get; set; }
        public int doubleKills { get; set; }
        public int dragonKills { get; set; }
        public bool eligibleForProgression { get; set; }
        public int enemyMissingPings { get; set; }
        public int enemyVisionPings { get; set; }
        public bool firstBloodAssist { get; set; }
        public bool firstBloodKill { get; set; }
        public bool firstTowerAssist { get; set; }
        public bool firstTowerKill { get; set; }
        public bool gameEndedInEarlySurrender { get; set; }
        public bool gameEndedInSurrender { get; set; }
        public int getBackPings { get; set; }
        public int goldEarned { get; set; }
        public int goldSpent { get; set; }
        public int holdPings { get; set; }
        public string? individualPosition { get; set; }
        public int inhibitorKills { get; set; }
        public int inhibitorTakedowns { get; set; }
        public int inhibitorsLost { get; set; }
        public int item0 { get; set; }
        public int item1 { get; set; }
        public int item2 { get; set; }
        public int item3 { get; set; }
        public int item4 { get; set; }
        public int item5 { get; set; }
        public int item6 { get; set; }
        public int itemsPurchased { get; set; }
        public int killingSprees { get; set; }
        public int kills { get; set; }
        public string? lane { get; set; }
        public int largestCriticalStrike { get; set; }
        public int largestKillingSpree { get; set; }
        public int largestMultiKill { get; set; }
        public int longestTimeSpentLiving { get; set; }
        public int magicDamageDealt { get; set; }
        public int magicDamageDealtToChampions { get; set; }
        public int magicDamageTaken { get; set; }
        public int needVisionPings { get; set; }
        public int neutralMinionsKilled { get; set; }
        public int nexusKills { get; set; }
        public int nexusLost { get; set; }
        public int nexusTakedowns { get; set; }
        public int objectivesStolen { get; set; }
        public int objectivesStolenAssists { get; set; }
        public int onMyWayPings { get; set; }
        public int participantId { get; set; }
        public int pentaKills { get; set; }
        public int physicalDamageDealt { get; set; }
        public int physicalDamageDealtToChampions { get; set; }
        public int physicalDamageTaken { get; set; }
        public int placement { get; set; }
        public int playerAugment1 { get; set; }
        public int playerAugment2 { get; set; }
        public int playerAugment3 { get; set; }
        public int playerAugment4 { get; set; }
        public int playerAugment5 { get; set; }
        public int playerAugment6 { get; set; }
        public int playerSubteamId { get; set; }
        public int profileIcon { get; set; }
        public int pushPings { get; set; }
        public string? puuid { get; set; }
        public int quadraKills { get; set; }
        public int retreatPings { get; set; }
        public string? riotIdGameName { get; set; }
        public string? riotIdTagline { get; set; }
        public string? role { get; set; }
        public int sightWardsBoughtInGame { get; set; }
        public int spell1Casts { get; set; }
        public int spell2Casts { get; set; }
        public int spell3Casts { get; set; }
        public int spell4Casts { get; set; }
        public int subteamPlacement { get; set; }
        public int summoner1Casts { get; set; }
        public int summoner1Id { get; set; }
        public int summoner2Casts { get; set; }
        public int summoner2Id { get; set; }
        public string? summonerId { get; set; }
        public int summonerLevel { get; set; }
        public string? summonerName { get; set; }
        public bool teamEarlySurrendered { get; set; }
        public int teamId { get; set; }
        public string? teamPosition { get; set; }
        public int timeCCingOthers { get; set; }
        public int timePlayed { get; set; }
        public int totalAllyJungleMinionsKilled { get; set; }
        public int totalDamageDealt { get; set; }
        public int totalDamageDealtToChampions { get; set; }
        public int totalDamageShieldedOnTeammates { get; set; }
        public int totalDamageTaken { get; set; }
        public int totalEnemyJungleMinionsKilled { get; set; }
        public int totalHeal { get; set; }
        public int totalHealsOnTeammates { get; set; }
        public int totalMinionsKilled { get; set; }
        public int totalTimeCCDealt { get; set; }
        public int totalTimeSpentDead { get; set; }
        public int totalUnitsHealed { get; set; }
        public int tripleKills { get; set; }
        public int trueDamageDealt { get; set; }
        public int trueDamageDealtToChampions { get; set; }
        public int trueDamageTaken { get; set; }
        public int turretKills { get; set; }
        public int turretTakedowns { get; set; }
        public int turretsLost { get; set; }
        public int unrealKills { get; set; }
        public int visionClearedPings { get; set; }
        public int visionScore { get; set; }
        public int visionWardsBoughtInGame { get; set; }
        public int wardsKilled { get; set; }
        public int wardsPlaced { get; set; }
        public bool win { get; set; }

        //Challanges

        public int _12AssistStreakCount { get; set; }
        public float HealFromMapSources { get; set; }
        public int InfernalScalePickup { get; set; }
        public int SWARM_DefeatAatrox { get; set; }
        public int SWARM_DefeatBriar { get; set; }
        public int SWARM_DefeatMiniBosses { get; set; }
        public int SWARM_EvolveWeapon { get; set; }
        public int SWARM_Have3Passives { get; set; }
        public int SWARM_KillEnemy { get; set; }
        public int SWARM_PickupGold { get; set; }
        public int SWARM_ReachLevel50 { get; set; }
        public int SWARM_Survive15Min { get; set; }
        public int SWARM_WinWith5EvolvedWeapons { get; set; }
        public int abilityUses { get; set; }
        public int acesBefore15Minutes { get; set; }
        public int alliedJungleMonsterKills { get; set; }
        public int baronTakedowns { get; set; }
        public int blastConeOppositeOpponentCount { get; set; }
        public float bountyGold { get; set; }
        public int buffsStolen { get; set; }
        public int completeSupportQuestInTime { get; set; }
        public float controlWardTimeCoverageInRiverOrEnemyHalf { get; set; }
        public int controlWardsPlaced { get; set; }
        public float damagePerMinute { get; set; }
        public float damageTakenOnTeamPercentage { get; set; }
        public int dancedWithRiftHerald { get; set; }
        public int deathsByEnemyChamps { get; set; }
        public int dodgeSkillShotsSmallWindow { get; set; }
        public int doubleAces { get; set; }
        public int dragonTakedowns { get; set; }
        public int earlyLaningPhaseGoldExpAdvantage { get; set; }
        public float effectiveHealAndShielding { get; set; }
        public int elderDragonKillsWithOpposingSoul { get; set; }
        public int elderDragonMultikills { get; set; }
        public int enemyChampionImmobilizations { get; set; }
        public int enemyJungleMonsterKills { get; set; }
        public int epicMonsterKillsNearEnemyJungler { get; set; }
        public int epicMonsterKillsWithin30SecondsOfSpawn { get; set; }
        public int epicMonsterSteals { get; set; }
        public int epicMonsterStolenWithoutSmite { get; set; }
        public int firstTurretKilled { get; set; }
        public int fistBumpParticipation { get; set; }
        public int flawlessAces { get; set; }
        public int fullTeamTakedown { get; set; }
        public float gameLength { get; set; }
        public int getTakedownsInAllLanesEarlyJungleAsLaner { get; set; }
        public float goldPerMinute { get; set; }
        public int hadOpenNexus { get; set; }
        public int highestWardKills { get; set; }
        public int immobilizeAndKillWithAlly { get; set; }
        public int initialBuffCount { get; set; }
        public int initialCrabCount { get; set; }
        public float jungleCsBefore10Minutes { get; set; }
        public int junglerTakedownsNearDamagedEpicMonster { get; set; }
        public int kTurretsDestroyedBeforePlatesFall { get; set; }
        public float kda { get; set; }
        public int killAfterHiddenWithAlly { get; set; }
        public float killParticipation { get; set; }
        public int killedChampTookFullTeamDamageSurvived { get; set; }
        public int killingSprees_Chellenges { get; set; }
        public int killsNearEnemyTurret { get; set; }
        public int killsOnOtherLanesEarlyJungleAsLaner { get; set; }
        public int killsOnRecentlyHealedByAramPack { get; set; }
        public int killsUnderOwnTurret { get; set; }
        public int killsWithHelpFromEpicMonster { get; set; }
        public int knockEnemyIntoTeamAndKill { get; set; }
        public int landSkillShotsEarlyGame { get; set; }
        public int laneMinionsFirst10Minutes { get; set; }
        public int laningPhaseGoldExpAdvantage { get; set; }
        public int legendaryCount { get; set; }
        public int lostAnInhibitor { get; set; }
        public float maxCsAdvantageOnLaneOpponent { get; set; }
        public int maxKillDeficit { get; set; }
        public int maxLevelLeadLaneOpponent { get; set; }
        public int mejaisFullStackInTime { get; set; }
        public float moreEnemyJungleThanOpponent { get; set; }
        public int multiKillOneSpell { get; set; }
        public int multiTurretRiftHeraldCount { get; set; }
        public int multikills { get; set; }
        public int multikillsAfterAggressiveFlash { get; set; }
        public int outerTurretExecutesBefore10Minutes { get; set; }
        public int outnumberedKills { get; set; }
        public int outnumberedNexusKill { get; set; }
        public int perfectDragonSoulsTaken { get; set; }
        public int perfectGame { get; set; }
        public int pickKillWithAlly { get; set; }
        public int playedChampSelectPosition { get; set; }
        public int poroExplosions { get; set; }
        public int quickCleanse { get; set; }
        public int quickFirstTurret { get; set; }
        public int quickSoloKills { get; set; }
        public int riftHeraldTakedowns { get; set; }
        public int saveAllyFromDeath { get; set; }
        public int scuttleCrabKills { get; set; }
        public int skillshotsDodged { get; set; }
        public int skillshotsHit { get; set; }
        public int snowballsHit { get; set; }
        public int soloBaronKills { get; set; }
        public int soloKills { get; set; }
        public int stealthWardsPlaced { get; set; }
        public int survivedSingleDigitHpCount { get; set; }
        public int survivedThreeImmobilizesInFight { get; set; }
        public int takedownOnFirstTurret { get; set; }
        public int takedowns { get; set; }
        public int takedownsAfterGainingLevelAdvantage { get; set; }
        public int takedownsBeforeJungleMinionSpawn { get; set; }
        public int takedownsFirstXMinutes { get; set; }
        public int takedownsInAlcove { get; set; }
        public int takedownsInEnemyFountain { get; set; }
        public int teamBaronKills { get; set; }
        public float teamDamagePercentage { get; set; }
        public int teamElderDragonKills { get; set; }
        public int teamRiftHeraldKills { get; set; }
        public int tookLargeDamageSurvived { get; set; }
        public int turretPlatesTaken { get; set; }
        public int turretTakedowns_Chellenges { get; set; }
        public int turretsTakenWithRiftHerald { get; set; }
        public int twentyMinionsIn3SecondsCount { get; set; }
        public int twoWardsOneSweeperCount { get; set; }
        public int unseenRecalls { get; set; }
        public float visionScoreAdvantageLaneOpponent { get; set; }
        public float visionScorePerMinute { get; set; }
        public int voidMonsterKill { get; set; }
        public int wardTakedowns { get; set; }
        public int wardTakedownsBefore20M { get; set; }
        public int wardsGuarded { get; set; }
        public float earliestDragonTakedown { get; set; }
        public int junglerKillsEarlyJungle { get; set; }
        public int killsOnLanersEarlyJungleAsJungler { get; set; }
        public int baronBuffGoldAdvantageOverThreshold { get; set; }
        public float earliestBaron { get; set; }
        public float firstTurretKilledTime { get; set; }
        public int highestChampionDamage { get; set; }
        public int soloTurretsLategame { get; set; }
        public int fasterSupportQuestCompletion { get; set; }
        public int highestCrowdControlScore { get; set; }

        //teams

        public int atakhan { get; set; }
        public bool atakhan_first { get; set; }
        public int baron { get; set; }
        public bool baron_first { get; set; }
        public int champion { get; set; }
        public bool champion_first { get; set; }
        public int dragon { get; set; }
        public bool dragon_first { get; set; }
        public int horde { get; set; }
        public bool horde_first { get; set; }
        public int inhibitor { get; set; }
        public bool inhibitor_first { get; set; }
        public int riftHerald { get; set; }
        public bool riftHerald_first { get; set; }
        public int tower { get; set; }
        public bool tower_first { get; set; }

        public Primary() { }
        public Primary(string? matchID, Participant participant, Challenges challenges, Objectives obj)
        {
            this.matchId = matchID;
            #region Participant
            PlayerScore0 = participant.PlayerScore0;
            PlayerScore1 = participant.PlayerScore1;
            PlayerScore10 = participant.PlayerScore10;
            PlayerScore11 = participant.PlayerScore11;
            PlayerScore2 = participant.PlayerScore2;
            PlayerScore3 = participant.PlayerScore3;
            PlayerScore4 = participant.PlayerScore4;
            PlayerScore5 = participant.PlayerScore5;
            PlayerScore6 = participant.PlayerScore6;
            PlayerScore7 = participant.PlayerScore7;
            PlayerScore8 = participant.PlayerScore8;
            PlayerScore9 = participant.PlayerScore9;
            allInPings = participant.allInPings;
            assistMePings = participant.assistMePings;
            assists = participant.assists;
            baronKills = participant.baronKills;
            basicPings = participant.basicPings;
            bountyLevel = participant.bountyLevel;
            champExperience = participant.champExperience;
            champLevel = participant.champLevel;
            championId = participant.championId;
            championName = participant.championName;
            championTransform = participant.championTransform;
            commandPings = participant.commandPings;
            consumablesPurchased = participant.consumablesPurchased;
            damageDealtToBuildings = participant.damageDealtToBuildings;
            damageDealtToObjectives = participant.damageDealtToObjectives;
            damageDealtToTurrets = participant.damageDealtToTurrets;
            damageSelfMitigated = participant.damageSelfMitigated;
            dangerPings = participant.dangerPings;
            deaths = participant.deaths;
            detectorWardsPlaced = participant.detectorWardsPlaced;
            doubleKills = participant.doubleKills;
            dragonKills = participant.dragonKills;
            eligibleForProgression = participant.eligibleForProgression;
            enemyMissingPings = participant.enemyMissingPings;
            enemyVisionPings = participant.enemyVisionPings;
            firstBloodAssist = participant.firstBloodAssist;
            firstBloodKill = participant.firstBloodKill;
            firstTowerAssist = participant.firstTowerAssist;
            firstTowerKill = participant.firstTowerKill;
            gameEndedInEarlySurrender = participant.gameEndedInEarlySurrender;
            gameEndedInSurrender = participant.gameEndedInSurrender;
            getBackPings = participant.getBackPings;
            goldEarned = participant.goldEarned;
            goldSpent = participant.goldSpent;
            holdPings = participant.holdPings;
            individualPosition = participant.individualPosition;
            inhibitorKills = participant.inhibitorKills;
            inhibitorTakedowns = participant.inhibitorTakedowns;
            inhibitorsLost = participant.inhibitorsLost;
            item0 = participant.item0;
            item1 = participant.item1;
            item2 = participant.item2;
            item3 = participant.item3;
            item4 = participant.item4;
            item5 = participant.item5;
            item6 = participant.item6;
            itemsPurchased = participant.itemsPurchased;
            killingSprees = participant.killingSprees;
            kills = participant.kills;
            lane = participant.lane;
            largestCriticalStrike = participant.largestCriticalStrike;
            largestKillingSpree = participant.largestKillingSpree;
            largestMultiKill = participant.largestMultiKill;
            longestTimeSpentLiving = participant.longestTimeSpentLiving;
            magicDamageDealt = participant.magicDamageDealt;
            magicDamageDealtToChampions = participant.magicDamageDealtToChampions;
            magicDamageTaken = participant.magicDamageTaken;
            needVisionPings = participant.needVisionPings;
            neutralMinionsKilled = participant.neutralMinionsKilled;
            nexusKills = participant.nexusKills;
            nexusLost = participant.nexusLost;
            nexusTakedowns = participant.nexusTakedowns;
            objectivesStolen = participant.objectivesStolen;
            objectivesStolenAssists = participant.objectivesStolenAssists;
            onMyWayPings = participant.onMyWayPings;
            participantId = participant.participantId;
            pentaKills = participant.pentaKills;
            physicalDamageDealt = participant.physicalDamageDealt;
            physicalDamageDealtToChampions = participant.physicalDamageDealtToChampions;
            physicalDamageTaken = participant.physicalDamageTaken;
            placement = participant.placement;
            playerAugment1 = participant.playerAugment1;
            playerAugment2 = participant.playerAugment2;
            playerAugment3 = participant.playerAugment3;
            playerAugment4 = participant.playerAugment4;
            playerAugment5 = participant.playerAugment5;
            playerAugment6 = participant.playerAugment6;
            playerSubteamId = participant.playerSubteamId;
            profileIcon = participant.profileIcon;
            pushPings = participant.pushPings;
            puuid = participant.puuid;
            quadraKills = participant.quadraKills;
            retreatPings = participant.retreatPings;
            riotIdGameName = participant.riotIdGameName;
            riotIdTagline = participant.riotIdTagline;
            role = participant.role;
            sightWardsBoughtInGame = participant.sightWardsBoughtInGame;
            spell1Casts = participant.spell1Casts;
            spell2Casts = participant.spell2Casts;
            spell3Casts = participant.spell3Casts;
            spell4Casts = participant.spell4Casts;
            subteamPlacement = participant.subteamPlacement;
            summoner1Casts = participant.summoner1Casts;
            summoner1Id = participant.summoner1Id;
            summoner2Casts = participant.summoner2Casts;
            summoner2Id = participant.summoner2Id;
            summonerId = participant.summonerId;
            summonerLevel = participant.summonerLevel;
            summonerName = participant.summonerName;
            teamEarlySurrendered = participant.teamEarlySurrendered;
            teamId = participant.teamId;
            teamPosition = participant.teamPosition;
            timeCCingOthers = participant.timeCCingOthers;
            timePlayed = participant.timePlayed;
            totalAllyJungleMinionsKilled = participant.totalAllyJungleMinionsKilled;
            totalDamageDealt = participant.totalDamageDealt;
            totalDamageDealtToChampions = participant.totalDamageDealtToChampions;
            totalDamageShieldedOnTeammates = participant.totalDamageShieldedOnTeammates;
            totalDamageTaken = participant.totalDamageTaken;
            totalEnemyJungleMinionsKilled = participant.totalEnemyJungleMinionsKilled;
            totalHeal = participant.totalHeal;
            totalHealsOnTeammates = participant.totalHealsOnTeammates;
            totalMinionsKilled = participant.totalMinionsKilled;
            totalTimeCCDealt = participant.totalTimeCCDealt;
            totalTimeSpentDead = participant.totalTimeSpentDead;
            totalUnitsHealed = participant.totalUnitsHealed;
            tripleKills = participant.tripleKills;
            trueDamageDealt = participant.trueDamageDealt;
            trueDamageDealtToChampions = participant.trueDamageDealtToChampions;
            trueDamageTaken = participant.trueDamageTaken;
            turretKills = participant.turretKills;
            turretTakedowns = participant.turretTakedowns;
            turretsLost = participant.turretsLost;
            unrealKills = participant.unrealKills;
            visionClearedPings = participant.visionClearedPings;
            visionScore = participant.visionScore;
            visionWardsBoughtInGame = participant.visionWardsBoughtInGame;
            wardsKilled = participant.wardsKilled;
            wardsPlaced = participant.wardsPlaced;
            win = participant.win;
            #endregion

            #region Challenges

            _12AssistStreakCount = challenges._12AssistStreakCount;
            HealFromMapSources = challenges.HealFromMapSources;
            InfernalScalePickup = challenges.InfernalScalePickup;
            SWARM_DefeatAatrox = challenges.SWARM_DefeatAatrox;
            SWARM_DefeatBriar = challenges.SWARM_DefeatBriar;
            SWARM_DefeatMiniBosses = challenges.SWARM_DefeatMiniBosses;
            SWARM_EvolveWeapon = challenges.SWARM_EvolveWeapon;
            SWARM_Have3Passives = challenges.SWARM_Have3Passives;
            SWARM_KillEnemy = challenges.SWARM_KillEnemy;
            SWARM_PickupGold = challenges.SWARM_PickupGold;
            SWARM_ReachLevel50 = challenges.SWARM_ReachLevel50;
            SWARM_Survive15Min = challenges.SWARM_Survive15Min;
            SWARM_WinWith5EvolvedWeapons = challenges.SWARM_WinWith5EvolvedWeapons;
            abilityUses = challenges.abilityUses;
            acesBefore15Minutes = challenges.acesBefore15Minutes;
            alliedJungleMonsterKills = challenges.alliedJungleMonsterKills;
            baronTakedowns = challenges.baronTakedowns;
            blastConeOppositeOpponentCount = challenges.blastConeOppositeOpponentCount;
            bountyGold = challenges.bountyGold;
            buffsStolen = challenges.buffsStolen;
            completeSupportQuestInTime = challenges.completeSupportQuestInTime;
            controlWardTimeCoverageInRiverOrEnemyHalf = challenges.controlWardTimeCoverageInRiverOrEnemyHalf;
            controlWardsPlaced = challenges.controlWardsPlaced;
            damagePerMinute = challenges.damagePerMinute;
            damageTakenOnTeamPercentage = challenges.damageTakenOnTeamPercentage;
            dancedWithRiftHerald = challenges.dancedWithRiftHerald;
            deathsByEnemyChamps = challenges.deathsByEnemyChamps;
            dodgeSkillShotsSmallWindow = challenges.dodgeSkillShotsSmallWindow;
            doubleAces = challenges.doubleAces;
            dragonTakedowns = challenges.dragonTakedowns;
            earlyLaningPhaseGoldExpAdvantage = challenges.earlyLaningPhaseGoldExpAdvantage;
            effectiveHealAndShielding = challenges.effectiveHealAndShielding;
            elderDragonKillsWithOpposingSoul = challenges.elderDragonKillsWithOpposingSoul;
            elderDragonMultikills = challenges.elderDragonMultikills;
            enemyChampionImmobilizations = challenges.enemyChampionImmobilizations;
            enemyJungleMonsterKills = challenges.enemyJungleMonsterKills;
            epicMonsterKillsNearEnemyJungler = challenges.epicMonsterKillsNearEnemyJungler;
            epicMonsterKillsWithin30SecondsOfSpawn = challenges.epicMonsterKillsWithin30SecondsOfSpawn;
            epicMonsterSteals = challenges.epicMonsterSteals;
            epicMonsterStolenWithoutSmite = challenges.epicMonsterStolenWithoutSmite;
            firstTurretKilled = challenges.firstTurretKilled;
            fistBumpParticipation = challenges.fistBumpParticipation;
            flawlessAces = challenges.flawlessAces;
            fullTeamTakedown = challenges.fullTeamTakedown;
            gameLength = challenges.gameLength;
            getTakedownsInAllLanesEarlyJungleAsLaner = challenges.getTakedownsInAllLanesEarlyJungleAsLaner;
            goldPerMinute = challenges.goldPerMinute;
            hadOpenNexus = challenges.hadOpenNexus;
            highestWardKills = challenges.highestWardKills;
            immobilizeAndKillWithAlly = challenges.immobilizeAndKillWithAlly;
            initialBuffCount = challenges.initialBuffCount;
            initialCrabCount = challenges.initialCrabCount;
            jungleCsBefore10Minutes = challenges.jungleCsBefore10Minutes;
            junglerTakedownsNearDamagedEpicMonster = challenges.junglerTakedownsNearDamagedEpicMonster;
            kTurretsDestroyedBeforePlatesFall = challenges.kTurretsDestroyedBeforePlatesFall;
            kda = challenges.kda;
            killAfterHiddenWithAlly = challenges.killAfterHiddenWithAlly;
            killParticipation = challenges.killParticipation;
            killedChampTookFullTeamDamageSurvived = challenges.killedChampTookFullTeamDamageSurvived;
            killingSprees = challenges.killingSprees;
            killsNearEnemyTurret = challenges.killsNearEnemyTurret;
            killsOnOtherLanesEarlyJungleAsLaner = challenges.killsOnOtherLanesEarlyJungleAsLaner;
            killsOnRecentlyHealedByAramPack = challenges.killsOnRecentlyHealedByAramPack;
            killsUnderOwnTurret = challenges.killsUnderOwnTurret;
            killsWithHelpFromEpicMonster = challenges.killsWithHelpFromEpicMonster;
            knockEnemyIntoTeamAndKill = challenges.knockEnemyIntoTeamAndKill;
            landSkillShotsEarlyGame = challenges.landSkillShotsEarlyGame;
            laneMinionsFirst10Minutes = challenges.laneMinionsFirst10Minutes;
            laningPhaseGoldExpAdvantage = challenges.laningPhaseGoldExpAdvantage;
            legendaryCount = challenges.legendaryCount;
            lostAnInhibitor = challenges.lostAnInhibitor;
            maxCsAdvantageOnLaneOpponent = challenges.maxCsAdvantageOnLaneOpponent;
            maxKillDeficit = challenges.maxKillDeficit;
            maxLevelLeadLaneOpponent = challenges.maxLevelLeadLaneOpponent;
            mejaisFullStackInTime = challenges.mejaisFullStackInTime;
            moreEnemyJungleThanOpponent = challenges.moreEnemyJungleThanOpponent;
            multiKillOneSpell = challenges.multiKillOneSpell;
            multiTurretRiftHeraldCount = challenges.multiTurretRiftHeraldCount;
            multikills = challenges.multikills;
            multikillsAfterAggressiveFlash = challenges.multikillsAfterAggressiveFlash;
            outerTurretExecutesBefore10Minutes = challenges.outerTurretExecutesBefore10Minutes;
            outnumberedKills = challenges.outnumberedKills;
            outnumberedNexusKill = challenges.outnumberedNexusKill;
            perfectDragonSoulsTaken = challenges.perfectDragonSoulsTaken;
            perfectGame = challenges.perfectGame;
            pickKillWithAlly = challenges.pickKillWithAlly;
            playedChampSelectPosition = challenges.playedChampSelectPosition;
            poroExplosions = challenges.poroExplosions;
            quickCleanse = challenges.quickCleanse;
            quickFirstTurret = challenges.quickFirstTurret;
            quickSoloKills = challenges.quickSoloKills;
            riftHeraldTakedowns = challenges.riftHeraldTakedowns;
            saveAllyFromDeath = challenges.saveAllyFromDeath;
            scuttleCrabKills = challenges.scuttleCrabKills;
            skillshotsDodged = challenges.skillshotsDodged;
            skillshotsHit = challenges.skillshotsHit;
            snowballsHit = challenges.snowballsHit;
            soloBaronKills = challenges.soloBaronKills;
            soloKills = challenges.soloKills;
            stealthWardsPlaced = challenges.stealthWardsPlaced;
            survivedSingleDigitHpCount = challenges.survivedSingleDigitHpCount;
            survivedThreeImmobilizesInFight = challenges.survivedThreeImmobilizesInFight;
            takedownOnFirstTurret = challenges.takedownOnFirstTurret;
            takedowns = challenges.takedowns;
            takedownsAfterGainingLevelAdvantage = challenges.takedownsAfterGainingLevelAdvantage;
            takedownsBeforeJungleMinionSpawn = challenges.takedownsBeforeJungleMinionSpawn;
            takedownsFirstXMinutes = challenges.takedownsFirstXMinutes;
            takedownsInAlcove = challenges.takedownsInAlcove;
            takedownsInEnemyFountain = challenges.takedownsInEnemyFountain;
            teamBaronKills = challenges.teamBaronKills;
            teamDamagePercentage = challenges.teamDamagePercentage;
            teamElderDragonKills = challenges.teamElderDragonKills;
            teamRiftHeraldKills = challenges.teamRiftHeraldKills;
            tookLargeDamageSurvived = challenges.tookLargeDamageSurvived;
            turretPlatesTaken = challenges.turretPlatesTaken;
            turretTakedowns = challenges.turretTakedowns;
            turretsTakenWithRiftHerald = challenges.turretsTakenWithRiftHerald;
            twentyMinionsIn3SecondsCount = challenges.twentyMinionsIn3SecondsCount;
            twoWardsOneSweeperCount = challenges.twoWardsOneSweeperCount;
            unseenRecalls = challenges.unseenRecalls;
            visionScoreAdvantageLaneOpponent = challenges.visionScoreAdvantageLaneOpponent;
            visionScorePerMinute = challenges.visionScorePerMinute;
            voidMonsterKill = challenges.voidMonsterKill;
            wardTakedowns = challenges.wardTakedowns;
            wardTakedownsBefore20M = challenges.wardTakedownsBefore20M;
            wardsGuarded = challenges.wardsGuarded;
            earliestDragonTakedown = challenges.earliestDragonTakedown;
            junglerKillsEarlyJungle = challenges.junglerKillsEarlyJungle;
            killsOnLanersEarlyJungleAsJungler = challenges.killsOnLanersEarlyJungleAsJungler;
            baronBuffGoldAdvantageOverThreshold = challenges.baronBuffGoldAdvantageOverThreshold;
            earliestBaron = challenges.earliestBaron;
            firstTurretKilledTime = challenges.firstTurretKilledTime;
            highestChampionDamage = challenges.highestChampionDamage;
            soloTurretsLategame = challenges.soloTurretsLategame;
            fasterSupportQuestCompletion = challenges.fasterSupportQuestCompletion;
            highestCrowdControlScore = challenges.highestCrowdControlScore;

            #endregion


            atakhan = obj.atakhan?.kills ?? 0;
            atakhan_first = obj.atakhan?.first ?? false;

            baron = obj.baron?.kills ?? 0;
            baron_first = obj.baron?.first ?? false;

            champion = obj.champion?.kills ?? 0;
            champion_first = obj.champion?.first ?? false;

            dragon = obj.dragon?.kills ?? 0;
            dragon_first = obj.dragon?.first ?? false;

            horde = obj.horde?.kills ?? 0;
            horde_first = obj.horde?.first ?? false;

            inhibitor = obj.inhibitor?.kills ?? 0;
            inhibitor_first = obj.inhibitor?.first ?? false;

            riftHerald = obj.riftHerald?.kills ?? 0;
            riftHerald_first = obj.riftHerald?.first ?? false;

            tower = obj.tower?.kills ?? 0;
            tower_first = obj.tower?.first ?? false;

        }
    }

    public class Secondary
    {


        public Rank rank { get; set; }
        public string? matchId { get; set; }
        public int allInPings { get; set; }
        public int assistMePings { get; set; }
        public int assists { get; set; }
        public int baronKills { get; set; }
        public int bountyLevel { get; set; }
        public int commandPings { get; set; }
        public int consumablesPurchased { get; set; }
        public int damageDealtToBuildings { get; set; }
        public int damageDealtToObjectives { get; set; }
        public int damageDealtToTurrets { get; set; }
        public int damageSelfMitigated { get; set; }
        public int deaths { get; set; }
        public int detectorWardsPlaced { get; set; }
        public int doubleKills { get; set; }
        public int dragonKills { get; set; }
        public bool eligibleForProgression { get; set; }
        public int enemyMissingPings { get; set; }
        public int enemyVisionPings { get; set; }
        public bool firstBloodAssist { get; set; }
        public bool firstBloodKill { get; set; }
        public bool firstTowerAssist { get; set; }
        public bool firstTowerKill { get; set; }
        public bool gameEndedInEarlySurrender { get; set; }
        public bool gameEndedInSurrender { get; set; }
        public int getBackPings { get; set; }
        public int goldEarned { get; set; }
        public int goldSpent { get; set; }
        public int holdPings { get; set; }
        public string? individualPosition { get; set; }
        public int inhibitorKills { get; set; }
        public int inhibitorTakedowns { get; set; }
        public int inhibitorsLost { get; set; }
        public int itemsPurchased { get; set; }
        public int killingSprees { get; set; }
        public int kills { get; set; }
        public string? lane { get; set; }
        public int largestCriticalStrike { get; set; }
        public int largestKillingSpree { get; set; }
        public int largestMultiKill { get; set; }
        public int longestTimeSpentLiving { get; set; }
        public int magicDamageDealt { get; set; }
        public int magicDamageDealtToChampions { get; set; }
        public int magicDamageTaken { get; set; }
        public int needVisionPings { get; set; }
        public int neutralMinionsKilled { get; set; }
        public int nexusKills { get; set; }
        public int nexusLost { get; set; }
        public int nexusTakedowns { get; set; }
        public int objectivesStolen { get; set; }
        public int objectivesStolenAssists { get; set; }
        public int onMyWayPings { get; set; }
        public int pentaKills { get; set; }
        public int physicalDamageDealt { get; set; }
        public int physicalDamageDealtToChampions { get; set; }
        public int physicalDamageTaken { get; set; }
        public int pushPings { get; set; }
        public string? puuid { get; set; }
        public int quadraKills { get; set; }
        public int retreatPings { get; set; }
        public string? riotIdGameName { get; set; }
        public string? riotIdTagline { get; set; }
        public string? role { get; set; }
        public int spell1Casts { get; set; }
        public int spell2Casts { get; set; }
        public int spell3Casts { get; set; }
        public int spell4Casts { get; set; }
        public int summoner1Casts { get; set; }
        public int summoner1Id { get; set; }
        public int summoner2Casts { get; set; }
        public int summoner2Id { get; set; }
        public string? summonerId { get; set; }
        public int summonerLevel { get; set; }
        public string? summonerName { get; set; }
        public bool teamEarlySurrendered { get; set; }
        public int timeCCingOthers { get; set; }
        public int timePlayed { get; set; }
        public int totalAllyJungleMinionsKilled { get; set; }
        public int totalDamageDealt { get; set; }
        public int totalDamageDealtToChampions { get; set; }
        public int totalDamageShieldedOnTeammates { get; set; }
        public int totalDamageTaken { get; set; }
        public int totalEnemyJungleMinionsKilled { get; set; }
        public int totalHeal { get; set; }
        public int totalHealsOnTeammates { get; set; }
        public int totalMinionsKilled { get; set; }
        public int totalTimeCCDealt { get; set; }
        public int totalTimeSpentDead { get; set; }
        public int totalUnitsHealed { get; set; }
        public int tripleKills { get; set; }
        public int trueDamageDealt { get; set; }
        public int trueDamageDealtToChampions { get; set; }
        public int trueDamageTaken { get; set; }
        public int turretKills { get; set; }
        public int turretTakedowns { get; set; }
        public int turretsLost { get; set; }
        public int visionClearedPings { get; set; }
        public int visionScore { get; set; }
        public int visionWardsBoughtInGame { get; set; }
        public int wardsKilled { get; set; }
        public int wardsPlaced { get; set; }
        public bool win { get; set; }

        //Challanges

        public float HealFromMapSources { get; set; }
        public int InfernalScalePickup { get; set; }
        public int abilityUses { get; set; }
        public int alliedJungleMonsterKills { get; set; }
        public int baronTakedowns { get; set; }
        public int blastConeOppositeOpponentCount { get; set; }
        public float bountyGold { get; set; }
        public int buffsStolen { get; set; }
        public float controlWardTimeCoverageInRiverOrEnemyHalf { get; set; }
        public int controlWardsPlaced { get; set; }
        public float damagePerMinute { get; set; }
        public float damageTakenOnTeamPercentage { get; set; }
        public int deathsByEnemyChamps { get; set; }
        public int dodgeSkillShotsSmallWindow { get; set; }
        public int dragonTakedowns { get; set; }
        public float effectiveHealAndShielding { get; set; }
        public int elderDragonKillsWithOpposingSoul { get; set; }
        public int elderDragonMultikills { get; set; }
        public int enemyChampionImmobilizations { get; set; }
        public int enemyJungleMonsterKills { get; set; }
        public int epicMonsterKillsNearEnemyJungler { get; set; }
        public int epicMonsterKillsWithin30SecondsOfSpawn { get; set; }
        public int epicMonsterSteals { get; set; }
        public int epicMonsterStolenWithoutSmite { get; set; }
        public int fullTeamTakedown { get; set; }
        public float gameLength { get; set; }
        public int getTakedownsInAllLanesEarlyJungleAsLaner { get; set; }
        public float goldPerMinute { get; set; }
        public int immobilizeAndKillWithAlly { get; set; }
        public int initialBuffCount { get; set; }
        public int initialCrabCount { get; set; }
        public float jungleCsBefore10Minutes { get; set; }
        public int junglerTakedownsNearDamagedEpicMonster { get; set; }
        public int kTurretsDestroyedBeforePlatesFall { get; set; }
        public float kda { get; set; }
        public int killAfterHiddenWithAlly { get; set; }
        public float killParticipation { get; set; }
        public int killedChampTookFullTeamDamageSurvived { get; set; }
        public int killsNearEnemyTurret { get; set; }
        public int killsOnOtherLanesEarlyJungleAsLaner { get; set; }
        public int killsUnderOwnTurret { get; set; }
        public int killsWithHelpFromEpicMonster { get; set; }
        public int knockEnemyIntoTeamAndKill { get; set; }
        public int landSkillShotsEarlyGame { get; set; }
        public int laneMinionsFirst10Minutes { get; set; }
        public int laningPhaseGoldExpAdvantage { get; set; }
        public int legendaryCount { get; set; }
        public float maxCsAdvantageOnLaneOpponent { get; set; }
        public int maxKillDeficit { get; set; }
        public int maxLevelLeadLaneOpponent { get; set; }
        public float moreEnemyJungleThanOpponent { get; set; }
        public int multiKillOneSpell { get; set; }
        public int multiTurretRiftHeraldCount { get; set; }
        public int multikills { get; set; }
        public int multikillsAfterAggressiveFlash { get; set; }
        public int outnumberedKills { get; set; }
        public int outnumberedNexusKill { get; set; }
        public int perfectDragonSoulsTaken { get; set; }
        public int perfectGame { get; set; }
        public int pickKillWithAlly { get; set; }
        public int quickCleanse { get; set; }
        public int quickFirstTurret { get; set; }
        public int quickSoloKills { get; set; }
        public int riftHeraldTakedowns { get; set; }
        public int saveAllyFromDeath { get; set; }
        public int scuttleCrabKills { get; set; }
        public int skillshotsDodged { get; set; }
        public int skillshotsHit { get; set; }
        public int soloBaronKills { get; set; }
        public int soloKills { get; set; }
        public int stealthWardsPlaced { get; set; }
        public int survivedSingleDigitHpCount { get; set; }
        public int survivedThreeImmobilizesInFight { get; set; }
        public int takedownOnFirstTurret { get; set; }
        public int takedowns { get; set; }
        public int takedownsAfterGainingLevelAdvantage { get; set; }
        public int takedownsBeforeJungleMinionSpawn { get; set; }
        public int takedownsFirstXMinutes { get; set; }
        public int takedownsInAlcove { get; set; }
        public int takedownsInEnemyFountain { get; set; }
        public int teamBaronKills { get; set; }
        public float teamDamagePercentage { get; set; }
        public int teamElderDragonKills { get; set; }
        public int teamRiftHeraldKills { get; set; }
        public int tookLargeDamageSurvived { get; set; }
        public int turretPlatesTaken { get; set; }
        public int turretsTakenWithRiftHerald { get; set; }
        public int twentyMinionsIn3SecondsCount { get; set; }
        public int twoWardsOneSweeperCount { get; set; }
        public float visionScoreAdvantageLaneOpponent { get; set; }
        public float visionScorePerMinute { get; set; }
        public int voidMonsterKill { get; set; }
        public int wardTakedowns { get; set; }
        public int wardTakedownsBefore20M { get; set; }
        public int wardsGuarded { get; set; }
        public float earliestDragonTakedown { get; set; }
        public int junglerKillsEarlyJungle { get; set; }
        public int killsOnLanersEarlyJungleAsJungler { get; set; }
        public int baronBuffGoldAdvantageOverThreshold { get; set; }
        public float earliestBaron { get; set; }
        public float firstTurretKilledTime { get; set; }
        public int highestChampionDamage { get; set; }
        public int soloTurretsLategame { get; set; }
        public int fasterSupportQuestCompletion { get; set; }
        public int highestCrowdControlScore { get; set; }

        //teams

        public int atakhan { get; set; }
        public bool atakhan_first { get; set; }
        public int baron { get; set; }
        public bool baron_first { get; set; }
        public int champion { get; set; }
        public bool champion_first { get; set; }
        public int dragon { get; set; }
        public bool dragon_first { get; set; }
        public int horde { get; set; }
        public bool horde_first { get; set; }
        public int inhibitor { get; set; }
        public bool inhibitor_first { get; set; }
        public int riftHerald { get; set; }
        public bool riftHerald_first { get; set; }
        public int tower { get; set; }
        public bool tower_first { get; set; }

        public Secondary() { }
        public Secondary(Rank rank, Primary primary)
        {
            this.rank = rank; 
            matchId = primary.matchId;
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
    }

    public class PlayerRank
    {
        public string? puuid { get; set; }
        public Rank Rank { get; set; }  

        public PlayerRank(string? puuid, Rank rank)
        {
            this.puuid = puuid;
            Rank = rank;
        }
    }



}
