import { mapQuestionFromServer } from './QuestionsData';
import { expect, test } from 'vitest';

test('When mapQuestionFromServer is called with question, created should be turned into a Date', () => {
  const result = mapQuestionFromServer({
    questionId: 1,
    title: 'test',
    content: 'test',
    userName: 'test',
    userId: '1',
    created: '2021-01-01T05:00:00.000Z',
    answers: [],
  });
  expect(result).toEqual({
    questionId: 1,
    title: 'test',
    content: 'test',
    userName: 'test',
    userId: '1',
    created: new Date(2021, 0, 1, 0, 0, 0, 0),
    answers: [],
  });
});

test('When mapQuestionFromServer is called with question and answers, created should be turned into a Date', () => {
  const result = mapQuestionFromServer({
    questionId: 1,
    title: 'test',
    content: 'test',
    userName: 'test',
    userId: '1',
    created: '2021-01-01T05:00:00.000Z',
    answers: [
      {
        answerId: 1,
        content: 'test',
        userName: 'test',
        created: '2021-01-01T05:00:00.000Z',
      },
    ],
  });
  expect(result).toEqual({
    questionId: 1,
    title: 'test',
    content: 'test',
    userName: 'test',
    userId: '1',
    created: new Date(2021, 0, 1, 0, 0, 0, 0),
    answers: [
      {
        answerId: 1,
        content: 'test',
        userName: 'test',
        created: new Date(2021, 0, 1, 0, 0, 0, 0),
      },
    ],
  });
});
