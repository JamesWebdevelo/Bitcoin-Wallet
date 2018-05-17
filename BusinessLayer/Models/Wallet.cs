using NBitcoin;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class Wallet
    {
        public Key PrivateKey { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Wallet()
        {

        }

        /// <summary>
        /// Method
        /// </summary>
        public void GenerateWallet()
        {
            AssertArgumentsLenght(args.Length, 1, 2);
            var walletFilePath = GetWalletFilePath(args);
            AssertWalletNotExists(walletFilePath);

            string pw;
            string pwConf;
            do
            {
                // 1. Get password from user
                WriteLine("Choose a password:");
                pw = PasswordConsole.ReadPassword();
                // 2. Get password confirmation from user
                WriteLine("Confirm password:");
                pwConf = PasswordConsole.ReadPassword();

                if (pw != pwConf) {
                    WriteLine("Passwords do not match. Try again!");
            } while (pw != pwConf);

            // 3. Create wallet
            Mnemonic mnemonic;

            Safe safe = Safe.Create(out mnemonic, pw, walletFilePath, Config.Network);
            // If no exception thrown the wallet is successfully created.
            WriteLine();
            WriteLine("Wallet is successfully created.");
            WriteLine($"Wallet file: {walletFilePath}");

            // 4. Display mnemonic
            //WriteLine();
            //WriteLine("Write down the following mnemonic words.");
            //WriteLine("With the mnemonic words AND your password you can recover this wallet by using the recover-wallet command.");
            //WriteLine();
            //WriteLine("-------");
            //WriteLine(mnemonic);
            //WriteLine("-------");
        }

        /// <summary>
        /// Method
        /// </summary>
        public void RecoverWallet()
        {

        }
    }
}
