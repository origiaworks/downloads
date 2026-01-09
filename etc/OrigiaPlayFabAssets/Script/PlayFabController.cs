using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabController : MonoBehaviour {
    [SerializeField]
    string customId;
    [SerializeField]
    string playFabId;

    /// <summary>
    /// ログイン
    /// </summary>
    /// <param name="customId"></param>
    public void LoginWithCustomID(string customId) {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        }, result =>
        {
            Debug.Log("ログインに成功しました");
            playFabId = result.PlayFabId;

            if (result.NewlyCreated) {
                Debug.Log("新しいアカウントを作成 ID:" + result.PlayFabId);
            }
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    /// <summary>
    /// タイトルデータの取得
    /// </summary>
    public void GetTitleData() {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result =>
            {
                Debug.Log("タイトルデータの取得に成功しました");
                //var questMaster = PlayFab.Json.PlayFabSimpleJson.DeserializeObject<List<QuestMaster>>(result.Data["QuestMaster"]);//example
            }, error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });
    }

    #region UserData
    /// <summary>
    /// ユーザーデータの更新
    /// </summary>
    public void UpdateUserData() {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                //{"ID",id }//example,
            },
            Permission = UserDataPermission.Public
        }, result =>
        {
            Debug.Log("ユーザーデータの更新に成功しました");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    /// <summary>
    /// ユーザーデータの取得
    /// </summary>
    /// <param name="playFabId"></param>
    public void GetUserData(string playFabId) {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = playFabId
        }, result =>
        {
            Debug.Log("ユーザーデータの取得に成功しました");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region Inventory

    /// <summary>
    /// インベントリーの取得
    /// </summary>
    private void GetUserInventry() {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest
        {
        }, (result) =>
        {
            Debug.Log("インベントリーの取得に成功しました");
            Debug.Log(result.Inventory);
        }, (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    /// <summary>
    /// カタログの取得
    /// </summary>
    /// <param name="catalogVersion"></param>
    public void GetCatalogData(string catalogVersion) {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {
            CatalogVersion = catalogVersion,
        }, result =>
        {
            Debug.Log("カタログの取得に成功しました");
            //CatalogItems = result.Catalog;//example
            //GetStoreData("Items", "gold_store");//example
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    /// <summary>
    /// ストアの取得
    /// </summary>
    /// <param name="catalogVersion"></param>
    /// <param name="storeId"></param>
    public void GetStoreItems(string catalogVersion, string storeId) {
        PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
        {
            CatalogVersion = catalogVersion,
            StoreId = storeId
        }, (result) =>
        {
            Debug.Log("ストアの取得に成功しました");
            //StoreItems = result.Store;//example
        }, (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    /// <summary>
    /// アイテムの購入
    /// </summary>
    /// <param name="catalogVersion"></param>
    /// <param name="storeId"></param>
    /// <param name="itemId"></param>
    /// <param name="virtualCurrency"></param>
    /// <param name="price"></param>
    public void PurchaseItem(string catalogVersion, string storeId, string itemId, string virtualCurrency, int price) {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
        {
            CatalogVersion = catalogVersion,
            StoreId = storeId,
            ItemId = itemId,
            VirtualCurrency = virtualCurrency,
            Price = price

        }, purchaseResult =>
        {
            Debug.Log(purchaseResult.Items[0].DisplayName + "の購入に成功しました");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
            if (error.Error == PlayFabErrorCode.InsufficientFunds) {
                Debug.Log("金額が不足のため購入できませんでした");
            }
        });
    }

    /// <summary>
    /// アイテムの使用
    /// </summary>
    /// <param name="instanceId"></param>
    /// <param name="consumeCount"></param>
    public void ConsumeItem(string instanceId, int consumeCount) {
        PlayFabClientAPI.ConsumeItem(new ConsumeItemRequest
        {
            ItemInstanceId = instanceId,
            ConsumeCount = consumeCount

        }, (result) =>
        {
            Debug.Log(result.ItemInstanceId + "を" + consumeCount + "個使用しました");
        }, (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region Leaderboard
    /// <summary>
    /// プレイヤー名(表示名)の更新
    /// </summary>
    /// <param name="displayName"></param>
    public void UpdateUserTitleDisplayName(string displayName) {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = displayName
            }, result =>
            {
                Debug.Log("プレイヤー名の更新に成功しました");
            }, error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    /// <summary>
    /// ランキングデータの送信
    /// </summary>
    /// <param name="leaderboardName"></param>
    /// <param name="score"></param>
    public void UpdatePlayerStatistics(string leaderboardName, int score) {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate> {
                    new StatisticUpdate {
                        StatisticName = leaderboardName,
                        Value = score
                    },
                }
            }, result =>
            {
                Debug.Log("ランキングデータの送信に成功しました");
            }, error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }

    /// <summary>
    /// ランキングの取得
    /// </summary>
    /// <param name="leaderboardName"></param>
    /// <param name="startPosition"></param>
    /// <param name="maxResultsCount"></param>
    public void GetLeaderboard(string leaderboardName, int startPosition, int maxResultsCount) {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest
            {
                StatisticName = leaderboardName,
                StartPosition = startPosition,
                MaxResultsCount = maxResultsCount
            }, result =>
            {
                Debug.Log("ランキングデータの取得に成功しました");
                foreach (PlayerLeaderboardEntry entry in result.Leaderboard) {
                    Debug.Log($"{entry.DisplayName}: {entry.StatValue}");
                }
            }, error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }
    #endregion

    #region VirtualCurrency
    /// <summary>
    /// 仮想通貨の取得
    /// </summary>
    public void GetVirtualCurrency() {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest()
        {
        }, result =>
        {
            Debug.Log("仮想通貨の取得に成功しました");
            var virtualCurrency = result.VirtualCurrency["**"];
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    /// <summary>
    /// 仮想通貨の増加
    /// </summary>
    /// <param name="virtualCurrency"></param>
    /// <param name="amount"></param>
    public void AddUserVirtualCurrency(string virtualCurrency, int amount) {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = virtualCurrency,
            Amount = amount
        }, result =>
        {
            Debug.Log("仮想通貨が増加しました");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    /// <summary>
    /// 仮想通貨の減少
    /// </summary>
    /// <param name="virtualCurrency"></param>
    /// <param name="amount"></param>
    public void SubtractUserVirtualCurrency(string virtualCurrency, int amount) {
        PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest()
        {
            VirtualCurrency = virtualCurrency,
            Amount = amount
        }, result =>
        {
            Debug.Log("仮想通貨が減少しました");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion
}
