type BaseApiResponse<T> = {
    succeeded: boolean;
    errors: any[];
    result: T;
}

export type SystemTask = {

        title: string;
        importance: number;
        isOff: boolean;
        id: string;
}
export type GetTasksResponse = BaseApiResponse<{
    tasks: SystemTask[]

}>
export type GetStateResponse = BaseApiResponse<{
    state: string

}>
