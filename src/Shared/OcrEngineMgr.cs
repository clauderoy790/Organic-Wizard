using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using Tesseract;

namespace Shared
{
    public class OcrEngineMgr
    {
        EngineConfig config;
        ConcurrentDictionary<TesseractEngine, bool> engines; //Engine with value if proccessing image or not

        public bool AllowCreateNewEngines { get; set; }
        public OcrEngineMgr(string dataPath, string language, EngineMode mode, int nbInstances)
        {
            config = new EngineConfig()
            {
                DataPath = dataPath,
                Language = language,
                Mode = mode
            };

            AllowCreateNewEngines = false;
            nbInstances = Math.Max(1, nbInstances);
            engines = new ConcurrentDictionary<TesseractEngine, bool>();
            for (int i = 0; i < nbInstances; i++)
                CreateEngine();
        }

        public Page Process(Bitmap bmp)
        {
            var engine = GetAvailableEngine();

            if (engine == null)
            {
                if (AllowCreateNewEngines)
                    engine = CreateEngine();
                else
                    throw new InvalidOperationException($"Cannot process image as all {engines.Count} engines are currently processing. Either set AllowCreateNewEngines to true to create an engine on the fly, allocate more engines at start or create a new engine calling the CreateNewEngine method.");
            }
            engines[engine] = true;
            Page page = engine.Process(bmp);
            engines[engine] = false;
            return page;
        }

        private TesseractEngine GetAvailableEngine()
        {
            TesseractEngine engine = null;

            foreach (var key in engines.Keys)
            {
                bool isProcessing;
                if (engines.TryGetValue(key, out isProcessing))
                {
                    if (!isProcessing)
                    {
                        engine = key;
                        break;
                    }
                }
            }

            return engine;
        }

        private TesseractEngine CreateEngine()
        {
            TesseractEngine engine = new TesseractEngine(config.DataPath, config.Language, config.Mode);
            if (!engines.TryAdd(engine, false))
                throw new Exception("Failed to register new engine.");
            return engine;
        }

        private struct EngineConfig
        {
            public string DataPath { get; set; }
            public string Language { get; set; }
            public EngineMode Mode { get; set; }
        }
    }
}
