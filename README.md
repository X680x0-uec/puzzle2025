# 2D Color Wall Puzzle Game

## セットアップ手順

1. Unityで新規プロジェクトを作成し、Assetsにこのスクリプトを追加。
2. PlayerとWallのPrefabを作成し、それぞれに`PlayerController`と`Wall`をアタッチ。
3. Playerの操作キーをInspectorで設定（例: Player1=WASD, Player2=矢印キー）。
4. GameManagerを空のGameObjectにアタッチし、Playerや色リストをInspectorで設定。
5. プレイして動作確認。

## ルール

- プレイヤーは上下左右に同時操作可能。
- 壁に当たるまで止まらず進む。
- プレイヤーと同じ色の壁はすり抜けることができる。
- プレイヤーの色はゲーム中に変化することがある。
