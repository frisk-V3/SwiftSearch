using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

Console.WriteLine("--- 高速ファイル検索ツール ---");

// 1. 検索条件の設定
Console.Write("検索するディレクトリパス (例: C:\\Users\\Name\\Documents): ");
string rootPath = Console.ReadLine() ?? "";

Console.Write("検索キーワード (ファイル名の一部): ");
string searchPattern = Console.ReadLine() ?? "";

if (!Directory.Exists(rootPath))
{
    Console.WriteLine("エラー: パスが見つかりません。");
    return;
}

Console.WriteLine("\n検索中... (しばらくお待ちください)");
var sw = Stopwatch.StartNew();
var foundFiles = new List<string>();

try
{
    // 2. 高速検索実行
    // EnumerateFilesはすべての結果を待たずに処理を開始できるため、GetFilesより効率的です
    var files = Directory.EnumerateFiles(rootPath, $"*{searchPattern}*", SearchOption.AllDirectories);

    foreach (var file in files)
    {
        foundFiles.Add(file);
    }
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("警告: アクセス権限のないフォルダをスキップしました。");
}
catch (Exception ex)
{
    Console.WriteLine($"エラーが発生しました: {ex.Message}");
}

sw.Stop();

// 3. 結果の表示（コピペ用リスト）
Console.WriteLine("\n" + new string('-', 30));
Console.WriteLine($"検索完了: {foundFiles.Count} 件ヒット ({sw.ElapsedMilliseconds}ms)");
Console.WriteLine(new string('-', 30));

if (foundFiles.Any())
{
    Console.WriteLine("【以下をコピーして利用してください】\n");
    foreach (var path in foundFiles)
    {
        Console.WriteLine(path);
    }
}
else
{
    Console.WriteLine("該当するファイルは見つかりませんでした。");
}

Console.WriteLine("\n" + new string('-', 30));
Console.WriteLine("終了するには何かキーを押してください...");
Console.ReadKey();