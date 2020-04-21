﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Test.Utilities;
using Xunit;

namespace Microsoft.CodeAnalysis.Editor.CSharp.UnitTests.Recommendations
{
    public class OrKeywordRecommenderTests : KeywordRecommenderTests
    {
        private const string InitializeObjectE = @"object e = new object();
";

#if !CODE_STYLE
        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAfterConstant()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"if (e is 1 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAfterMultipleConstants()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"if (e is 1 or 2 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAfterType()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"if (e is int $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAfterRelationalOperator()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"if (e is >= 0 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestInsideSubpattern()
        {
            await VerifyKeywordAsync(
@"class C
{
    public int P { get; }

    void M(C test)
    {
        if (test is { P: 1 $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestInsideSubpattern_AfterOpenParen()
        {
            await VerifyKeywordAsync(
@"class C
{
    int P { get; }

    void M()
    {
        var C2 = new C();
        if (C2 is { P: (1 $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestInsideSubpattern_AfterMultipleOpenParens()
        {
            await VerifyKeywordAsync(
@"class C
{
    int P { get; }

    void M()
    {
        var C2 = new C();
        if (C2 is { P: (((1 $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAtBeginningOfSwitchStatement_AfterMultipleOpenParens_MemberAccessExpression()
        {
            await VerifyKeywordAsync(
@"namespace N
{
    class C
    {
        const int P = 1;

        void M()
        {
            var e = new object();
            switch (e)
            {
                case (((N.C.P $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAtBeginningOfSwitchStatement_AfterMultipleOpenParens_MemberAccessExpression2()
        {
            await VerifyKeywordAsync(
@"namespace N
{
    class C
    {
        void M()
        {
            var e = new object();
            switch (e)
            {
                case (((N.C $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestInsideSubpattern_AfterParenPair()
        {
            await VerifyKeywordAsync(
@"class C
{
    int P { get; }

    void M()
    {
        var C2 = new C();
        if (C2 is { P: (1) $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestInsideSubpattern_AfterMultipleParenPairs()
        {
            await VerifyKeywordAsync(
@"class C
{
    int P { get; }

    void M()
    {
        var C2 = new C();
        if (C2 is { P: (((1))) $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAfterQualifiedName()
        {
            await VerifyKeywordAsync(
@"class C
{
    int P { get; }

    void M()
    {
        var C2 = new C();
        var e = new object();
        if (e is C2.P $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAfterQualifiedName2()
        {
            await VerifyKeywordAsync(
@"
namespace N
{
    class C
    {
        const int P = 1;

        void M()
        {
            var e = new object();
            if (e is N.C.P $$");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAtBeginningOfSwitchExpression()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"var result = e switch
{
    1 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAtBeginningOfSwitchStatement()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"switch (e)
{
    case 1 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAtBeginningOfSwitchExpression_AfterOpenParen()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"var result = e switch
{
    (1 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAtBeginningOfSwitchExpression_AfterMultipleOpenParens()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"var result = e switch
{
    (((1 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAtBeginningOfSwitchStatement_AfterOpenParen()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"switch (e)
{
    case (1 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestAtBeginningOfSwitchStatement_AfterMultipleOpenParens()
        {
            await VerifyKeywordAsync(AddInsideMethod(InitializeObjectE +
@"switch (e)
{
    case (((1 $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAfterIsKeyword()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"if (e is $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAfterNotKeyword()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"if (e is not $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAfterAndKeyword()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"if (e is 1 and $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAfterOrKeyword()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"if (e is 1 or $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAfterOpenParen()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"if (e is ($$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAtBeginningOfSwitchExpression()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"var result = e switch
{
    $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAtBeginningOfSwitchStatement()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"switch (e)
{
    case $$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAfterTypeAndOpenParen()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"if (e is int ($$"));
        }

        [Fact, Trait(Traits.Feature, Traits.Features.KeywordRecommending)]
        public async Task TestMissingAfterTypeAndCloseParen()
        {
            await VerifyAbsenceAsync(AddInsideMethod(InitializeObjectE +
@"if (e is int)$$"));
        }
#endif
    }
}
