﻿using System;
using System.Collections.Generic;
using System.Configuration;

namespace hoTools.Settings
{
    /// <summary>
    /// Administer sql history file names of config file
    /// </summary>
    public class SqlHistoryFilesCfg
    {
        const string SQL_HISTORY_FILE_CFG_STRING = "SqlFile";
        /// <summary>
        /// List of files loaded in history
        /// </summary>
        public List<string> lSqlHistoryFilesCfg => _lSqlHistoryFilesCfg;
        List<string> _lSqlHistoryFilesCfg = new List<string>();


        int _lSqlFilesCfgLength;
        Configuration _config;

        /// <summary>
        /// Constructor which load all sql history file names
        /// </summary>
        /// <param name="currentConfig">current Configuration</param>
        public SqlHistoryFilesCfg(Configuration currentConfig)
        {
            _config = currentConfig;
            load();
        }

        /// <summary>
        ///  Loads sql history file names from configuration
        /// </summary>
        public void load()
        {
            // SqlFile<i> i=1..20
            foreach (KeyValueConfigurationElement entry in _config.AppSettings.Settings)
            {
                // find key
                string key = entry.Key;
                if (key.Substring(0,7).Equals(SQL_HISTORY_FILE_CFG_STRING))
                {
                    _lSqlHistoryFilesCfg.Add(entry.Value);


                }
            }
            _lSqlFilesCfgLength = _lSqlHistoryFilesCfg.Count;
        }
        /// <summary>
        /// Save sql file names to configuration
        /// </summary>
        public void save()
        {
            int i = 1;
            foreach (string s in _lSqlHistoryFilesCfg)
            {
                // SqlFile<i>
                string key = $"{SQL_HISTORY_FILE_CFG_STRING}{i}";
                _config.AppSettings.Settings[key].Value = s;
                i = i + 1;
                // stop if element length reached
                if (i > _lSqlFilesCfgLength) break;
            }
        }
        /// <summary>
        /// Insert an SQL File to the beginning, an 
        /// </summary>
        /// <param name="s"></param>
        public void insert(string s)
        {
            // delete an existing entry
            // add to first one
            _lSqlHistoryFilesCfg.Remove(s);
            _lSqlHistoryFilesCfg.Insert(0, s);

        }
    }
}