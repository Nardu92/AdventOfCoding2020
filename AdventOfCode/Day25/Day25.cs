﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day25
    {
        public static long Solution1()
        {
            var door = new HandshakeParty(18356117);
            var card = new HandshakeParty(5909654);

            return door.GetEncryptionKey(card.PublicKey);
        }
        
    }

    public class HandshakeParty
    {
        public long SubjectNumber { get; private set; }
        public long PublicKey { get; private set; }
        public long EncryptionKey { get; private set; }
        public int LoopSize { get; private set; }

        public HandshakeParty(long publicKey)
        {
            SubjectNumber = 7;
            PublicKey = publicKey;
            CalculateLoopSize();
        }

        private void CalculateLoopSize()
        {
            long value = 1;
            int loopSize = 0;
            while (value != PublicKey)
            {
                value = TransformSubjectNumber(SubjectNumber, value);
                loopSize++;
            }
            LoopSize = loopSize;
        }

        public long GetEncryptionKey(long publicKey)
        {
            long value = 1;
            for (int i = 0; i < LoopSize; i++)
            {
                value = TransformSubjectNumber(publicKey, value);
            }
            EncryptionKey = value;
            return value;
        }

        long LoopSizeHash = 20201227;
        private long TransformSubjectNumber(long subjectNumber, long value)
        {
            value = value * subjectNumber;
            value =  value % LoopSizeHash;
            return value;
        }
    }
}
